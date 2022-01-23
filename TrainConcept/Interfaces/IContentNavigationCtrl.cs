namespace SoftObject.TrainConcept.Interfaces
{
    public interface IContentNavigationCtrl
    {
        void SetPageId(int iPageId);
        int  GetPageId();
        void SetMaxPages(int iPages);
        void SetVideo(string strVideoPath);
        void SetAnimation(string strAnimPath1, string strAnimPath2);
        void SetGrafic(int iImgId,int iImgCnt);
        void SetSimulation(bool bIsMill, string strSimulPath1, string strSimulPath2);
        void AddDocument(string strTitle, int typeId);
        void ClearDocuments();
    }
}
