using System;
using System.Collections.Generic;
using System.IO;
using DataGathererRobot.WebsiteInformationGatherer.Entities;
using Microsoft.Office.Interop.Excel;

namespace DataGathererRobot.ExcelDataFileCreator
{
    public class MicrowavesExcelFileCreator : IExcelFileCreator
    {
        private const string Path = "C:\\Temp";
        private const string WorksheetName = "Microwaves";
        private const string FileName = "microwaves";

        private readonly Application _excel;

        public MicrowavesExcelFileCreator()
        {
            CreateDirectoryIfNotExists();
            _excel = new Application();
            _excel.Visible = false;
            _excel.DisplayAlerts = false;
        }

        public void CreateFile<T>(List<T> items)
        {
            List<Microwave> microwaves = items as List<Microwave>;

            Workbook workbook = _excel.Workbooks.Add(Type.Missing);
            Worksheet worksheet = (Worksheet)workbook.ActiveSheet;
            worksheet.Name = WorksheetName;

            int i = 1;
            foreach (var microwave in microwaves)
            {
                worksheet.Cells[i, 1] = microwave.Name;
                worksheet.Cells[i, 2] = microwave.Price;
                worksheet.Cells[i, 3] = microwave.Link;
                i++;
            }

            Range cellRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[microwaves.Count, 3]];
            cellRange.EntireColumn.AutoFit();

            workbook.SaveAs(Path + "\\" + FileName);
            workbook.Close();
            _excel.Quit();
        }

        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
    }
}
