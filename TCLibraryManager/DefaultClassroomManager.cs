using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public class DefaultClassroomManager : IClassroomManager
    {
        private string m_fileName;
        private ArrayList m_aClassrooms = new ArrayList();
        private bool m_isOpen = false;
        private IClassroomAdapter m_adapter = null;
        private ClassroomManagerBridge m_parent = null;

        public void SetParent(object parent)
        {
            m_parent = (ClassroomManagerBridge)parent;
        }

        public void SetAdapter(IClassroomAdapter adapter)
        {
            m_adapter = adapter;
        }

        public bool Open(string fileName)
        {
            if (m_isOpen)
                Close();

            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ClassroomList));
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                ClassroomList cl;
                cl = (ClassroomList)serializer.Deserialize(fs);

                ClassroomItem[] aClassrooms = cl.aClassrooms;
                foreach (ClassroomItem item in aClassrooms)
                    m_aClassrooms.Add(item);
                fs.Close();

                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.Open, fileName, 0);
                m_parent?.FireEvent(ref ea);
                m_isOpen = true;
                return true;
            }
            catch (Exception)
            {
                m_isOpen = false;
                return false;
            }
            finally
            {
                m_fileName = fileName;
            }
        }

        public void Close()
        {
            if (m_isOpen)
                Save();
            m_aClassrooms.Clear();
            m_fileName = "";
            m_isOpen = false;
            ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.Close, m_fileName, 0);
            m_parent?.FireEvent(ref ea);
        }

        public void Save(string fileName)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                if (m_aClassrooms.Count > 0 && fileName.Length > 0)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ClassroomList));
                    TextWriter writer = new StreamWriter(fileName);

                    ClassroomList cl = new ClassroomList();
                    cl.aClassrooms = new ClassroomItem[m_aClassrooms.Count];
                    int i = 0;
                    foreach (ClassroomItem item in m_aClassrooms)
                    {
                        item.IsModified = false;
                        cl.aClassrooms.SetValue(item, i++);
                    }

                    // Serialize the List, and close the TextWriter.
                    serializer.Serialize(writer, cl);
                    writer.Close();
                    m_fileName = fileName;

                    ClassroomManagerEventArgs ea =
                        new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.Save, fileName, 0);
                    m_parent?.FireEvent(ref ea);
                }
            }
            catch (Exception ex)
            {
                //string txt=AppHandler.LanguageHandler.GetText("ERROR","Failed_to_save_userfile","Fehler beim Speichern der Userdatei!");
                //string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
                //MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
                String strText = ex.Message;
                if (m_adapter != null)
                    m_adapter.ShowMessageBox("ERROR", "Failed_to_save_classroomfile",
                        "Fehler beim Speichern der Klassendatei!");
                else
                {
                    Console.WriteLine(strText);
                }
            }
            finally
            {
                m_fileName = fileName;
            }
        }

        public void Save()
        { 
            Save(m_fileName);
        }

        public bool IsOpen()
        {
            return m_isOpen;
        }

        public string FileName()
        {
            return m_fileName;
        }

        public bool OpenFromImport(string classesFolder, string fileName)
        {


            return true;
        }
        
        public int CreateClassroom(string className)
        {
            if (GetClassId(className) >= 0)
                return -1;
            if (m_aClassrooms.Add(new ClassroomItem(className,true)) >= 0)
            {
                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.Create, className, 0);
                m_parent?.FireEvent(ref ea);
                return m_aClassrooms.Count;
            }
            return -1;
        }

        public bool DeleteClassroom(string className)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                m_aClassrooms.RemoveAt(id);
                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.Destroy, className, 0);
                m_parent?.FireEvent(ref ea);

                return true;
            }
            return false;
        }

        public bool AddLearnmap(string className,string mapName)
        {
            int id=GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.aLearnmaps == null)
                    ci.aLearnmaps = new string[1];
                else
                {
                    if (Array.Find(ci.aLearnmaps, p => p == mapName) != null)
                        return false;
                    String[] aNewItems = new String[ci.aLearnmaps.Length + 1];
                    ci.aLearnmaps.CopyTo(aNewItems, 0);
                    ci.aLearnmaps = aNewItems;
                }

                ci.aLearnmaps[ci.aLearnmaps.Length - 1] = mapName;
                ci.IsModified = true;
                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.AddLearnmap, mapName, 0);
                m_parent?.FireEvent(ref ea);

                return true;
            }
            return false;
        }

        public bool RemoveLearnmap(string className, string mapName)
        {
            int id=GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;

                if (ci.aLearnmaps.Length == 0 || ci.aLearnmaps == null)
                    return false;
                else
                {
                    String[] aNewItems = new String[ci.aLearnmaps.Length - 1];
                    for (int i = 0, j = 0; i < ci.aLearnmaps.Length; ++i)
                    {
                        if (ci.aLearnmaps[i]!=mapName)
                            aNewItems[j++] = ci.aLearnmaps[i];
                    }

                    if (aNewItems.Length < ci.aLearnmaps.Length)
                    {
                        ci.aLearnmaps = aNewItems;
                        ci.IsModified = true;
                        ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.RemoveLearnmap, mapName, 0);
                        m_parent?.FireEvent(ref ea);

                        return true;
                    }  
                }
            }
            return false;
        }

        public int GetLearnmapNames(string className, out string[] aNames)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.aLearnmaps != null && ci.aLearnmaps.Length > 0)
                {
                    int i = 0;
                    aNames = new string[ci.aLearnmaps.Length];
                    foreach (String s in ci.aLearnmaps)
                        aNames[i++] = s;
                    return aNames.Length;
                }
            }
            aNames = null;
            return 0;
        }

        public bool HasLearnmap(string className, string mapName)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.aLearnmaps != null)
                {
                    if (Array.Find(ci.aLearnmaps, p => p == mapName) != null)
                        return true;
                }
            }
            return false;
        }


        public int GetClassCount()
        {
            return m_aClassrooms.Count;
        }

        public int GetClassId(string className)
        {
            for (int i = 0; i < m_aClassrooms.Count; ++i)
                if ((m_aClassrooms[i] as ClassroomItem).className == className)
                    return i;
            return -1;
        }

        public int GetClassNames(out string[] aNames)
        {
            aNames = null;
            if (m_aClassrooms.Count > 0)
            {
                aNames = new string[m_aClassrooms.Count];
                int i = 0;
                foreach (ClassroomItem ci in m_aClassrooms)
                    aNames[i++] = ci.className;
                return aNames.Count();
            }
            return 0;
        }


        public bool AddUser(string className, string userName)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.aUsers == null)
                    ci.aUsers = new string[1];
                else
                {
                    if (Array.Find(ci.aUsers, p => p == userName) != null)
                        return false;
                    String[] aNewItems = new String[ci.aUsers.Length + 1];
                    ci.aUsers.CopyTo(aNewItems, 0);
                    ci.aUsers = aNewItems;
                }

                ci.aUsers[ci.aUsers.Length - 1] = userName;
                ci.IsModified = true;
                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.AddUser, userName, 0);
                m_parent?.FireEvent(ref ea);

                return true;
            }
            return false;
        }

        public bool RemoveUser(string className, string userName)
        {
            if (!HasUser(className, userName))
                return false;

            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;

                if (ci.aUsers.Length == 0 || ci.aUsers == null)
                    return false;
                else
                {
                    String[] aNewItems = new String[ci.aUsers.Length - 1];
                    for (int i = 0, j = 0; i < ci.aUsers.Length; ++i)
                    {
                        if (ci.aUsers[i] != userName)
                            aNewItems[j++] = ci.aUsers[i];
                    }

                    if (aNewItems.Length < ci.aUsers.Length)
                    {
                        ci.aUsers = aNewItems;
                        ci.IsModified = true;
                        ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.RemoveUser, userName, 0);
                        m_parent?.FireEvent(ref ea);

                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveUser(string userName)
        {
            bool bRes = false;
            for (int i = 0; i < m_aClassrooms.Count; ++i)
                bRes |= RemoveUser((m_aClassrooms[i] as ClassroomItem).className, userName);
            return bRes;
        }

        public int GetUserNames(string className, out string[] aNames)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.aUsers != null && ci.aUsers.Length > 0)
                {
                    int i = 0;
                    aNames = new string[ci.aUsers.Length];
                    foreach (String s in ci.aUsers)
                        aNames[i++] = s;
                    return aNames.Length;
                }
            }
            aNames = null;
            return 0;
        }


        public bool SetLearnmapNames(string className, string[] aNames)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                ci.aLearnmaps = aNames;
                ci.IsModified = true;
                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.AddLearnmap, "", 0);
                m_parent?.FireEvent(ref ea);
                return true;
            }
            return false;
        }

        public bool SetUserNames(string className, string[] aNames)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                ci.aUsers = aNames;
                ci.IsModified = true;
                ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.AddUser, "", 0);
                m_parent?.FireEvent(ref ea);
                return true;
            }
            return false;
        }

        public bool SetClassMapUsage(string className, bool isOn)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.useClassMaps != isOn)
                {
                    ci.useClassMaps = isOn;
                    ci.IsModified = true;
                    ClassroomManagerEventArgs ea = new ClassroomManagerEventArgs(ClassroomManagerEventArgs.CommandType.ChangeUsage, "", 0);
                    m_parent?.FireEvent(ref ea);
                    return true;
                }
            }
            return false;
        }

        public bool GetClassMapUsage(string className, ref bool isOn)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                isOn = ci.useClassMaps;
                return true;
            }
            return false;
        }

        public bool IsUsedAsClassMap(string mapName,string userName)
        {
            foreach (ClassroomItem ci in m_aClassrooms)
                if (ci.aLearnmaps != null)
                    if (ci.useClassMaps && Array.Find(ci.aLearnmaps, p => p == mapName) != null)
                        return (ci.aUsers!=null && Array.Find(ci.aUsers, p => p == userName) != null);
            return false;
        }

        public bool HasUser(string className, string userName)
        {
            int id = GetClassId(className);
            if (id >= 0)
            {
                ClassroomItem ci = m_aClassrooms[id] as ClassroomItem;
                if (ci.aUsers != null)
                {
                    if (Array.Find(ci.aUsers, p => p == userName) != null)
                        return true;
                }
            }
            return false;
        }
    }
}
