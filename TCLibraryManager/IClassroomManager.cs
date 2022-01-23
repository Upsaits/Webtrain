using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public delegate void OnClassroomManagerHandler(object sender, ref ClassroomManagerEventArgs ea);

    public interface IClassroomManager
    {
        void SetParent(object parent);
        void SetAdapter(IClassroomAdapter adapter);
        bool Open(string fileName);
        void Close();
        void Save(string fileName);
        void Save();
        bool IsOpen();
        string FileName();
        bool OpenFromImport(string classesFolder, string fileName);
        int CreateClassroom(string className);
        bool DeleteClassroom(string className);
        bool AddLearnmap(string className,string mapName);
        bool RemoveLearnmap(string className, string mapName);
        bool SetLearnmapNames(string className, string[] aNames);
        int GetLearnmapNames(string className, out string[] aNames);
        bool HasLearnmap(string className, string mapName);
        bool IsUsedAsClassMap(string mapName, string userName);
        bool AddUser(string className, string userName);
        bool RemoveUser(string className, string userName);
        bool RemoveUser(string userName);
        bool SetUserNames(string className, string[] aNames);
        int GetUserNames(string className, out string[] aNames);
        bool HasUser(string className, string userName);
        bool SetClassMapUsage(string className, bool isOn);
        bool GetClassMapUsage(string className,ref bool isOn);
        int GetClassId(string className);
        int GetClassCount();
        int GetClassNames(out string[] aNames);
    }
}
