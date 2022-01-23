using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public class ClassroomManagerBridge
    {
        public event OnClassroomManagerHandler ClassroomManagerEventHandler;
        public event OnClassroomManagerHandler ClassroomManagerEvent
        {
            add { ClassroomManagerEventHandler += value; }
            remove { ClassroomManagerEventHandler -= value; }
        }

        private IClassroomManager m_imp;

        public ClassroomManagerBridge(IClassroomManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool Open(string fileName)
        {
            return m_imp.Open(fileName);
        }

        public void Close()
        {
            m_imp.Close();
        }

        public void Save(string fileName)
        {
            m_imp.Save(fileName);
        }

        public void Save()
        {
            m_imp.Save();
        }

        public bool IsOpen()
        {
            return m_imp.IsOpen();
        }

        public string FileName()
        {
            return m_imp.FileName();
        }

        public bool OpenFromImport(string classesFolder, string fileName)
        {
            return m_imp.OpenFromImport(classesFolder, fileName);
        }

        public void SetAdapter(IClassroomAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public int CreateClassroom(string className)
        {
            return m_imp.CreateClassroom(className);
        }

        public bool DeleteClassroom(string className)
        {
            return m_imp.DeleteClassroom(className);
        }

        public bool AddLearnmap(string className, string mapName)
        {
            return m_imp.AddLearnmap(className, mapName);
        }

        public bool RemoveLearnmap(string className, string mapName) 
        {
            return m_imp.RemoveLearnmap(className, mapName);
        }

        public int GetLearnmapNames(string className, out string[] aNames)
        {
            return m_imp.GetLearnmapNames(className, out aNames);
        }

        public bool SetLearnmapNames(string className, string[] aNames)
        {
            return m_imp.SetLearnmapNames(className, aNames);
        }

        public bool HasLearnmap(string className, string mapName)
        {
            return m_imp.HasLearnmap(className, mapName);
        }

        public bool IsUsedAsClassMap(string mapName, string userName)
        {
            return m_imp.IsUsedAsClassMap(mapName, userName);
        }

        public bool AddUser(string className, string userName)
        {
            return m_imp.AddUser(className, userName);
        }

        public bool RemoveUser(string className, string userName)
        {
            return m_imp.RemoveUser(className, userName);
        }

        public bool RemoveUser(string userName)
        {
            return m_imp.RemoveUser(userName);
        }

        public int GetUserNames(string className, out string[] aNames)
        {
            return m_imp.GetUserNames(className, out aNames);
        }

        public bool SetUserNames(string className, string[] aNames)
        {
            return m_imp.SetUserNames(className, aNames);
        }

        public bool HasUser(string className, string userName)
        {
            return m_imp.HasUser(className, userName);
        }

        public bool SetClassMapUsage(string className, bool isOn)
        {
            return m_imp.SetClassMapUsage(className, isOn);
        }

        public bool GetClassMapUsage(string className, ref bool isOn)
        {
            return m_imp.GetClassMapUsage(className,ref isOn);
        }

        public int GetClassId(string className)
        {
            return m_imp.GetClassId(className);
        }

        public int GetClassCount()
        {
            return m_imp.GetClassCount();
        }

        public int GetClassNames(out string[] aNames)
        {
            return m_imp.GetClassNames(out aNames);
        }

        public void FireEvent(ref ClassroomManagerEventArgs ea)
        {
            if (ClassroomManagerEventHandler != null)
                ClassroomManagerEventHandler(this, ref ea);
        }

    }


}
