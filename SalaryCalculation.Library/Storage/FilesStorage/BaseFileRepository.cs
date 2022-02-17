namespace SalaryCalculation.Library.Storage.FileStorage
{
    public class BaseFileRepository
    {
        public FilesInfo FilesInfo { get; private set; }
        public BaseFileRepository(FilesInfo filesInfo)
        {
            FilesInfo = filesInfo;
        }
    }
}
