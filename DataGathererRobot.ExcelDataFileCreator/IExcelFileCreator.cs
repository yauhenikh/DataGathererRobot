using System.Collections.Generic;

namespace DataGathererRobot.ExcelDataFileCreator
{
    public interface IExcelFileCreator
    {
        void CreateFile<T>(List<T> items);
    }
}
