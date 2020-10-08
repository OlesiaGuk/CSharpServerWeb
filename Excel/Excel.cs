using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Excel
{
    class Excel
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var personsList = new List<Person>
            {
                new Person("Иванов", "Иван", 36, "9131234567"),
                new Person("Петров", "Петр", 22, "9132345678"),
                new Person("Сидоров", "Михаил", 25, "9133456789"),
                new Person("Матвеев", "Алексей", 50, "+810425698547826")
            };

            var headers = new List<string> { "Фамилия", "Имя", "Возраст", "Номер телефона" };

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Persons");

                for (var i = 0; i < headers.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                worksheet.Cells[2, 1].LoadFromCollection(personsList);

                worksheet.Cells[1, 1, 1, headers.Count].Style.Font.Bold = true;
                worksheet.Cells[1, 1, personsList.Count + 1, headers.Count].AutoFitColumns();
                worksheet.Cells[1, 1, personsList.Count + 1, headers.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[1, 1, personsList.Count + 1, headers.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                excelPackage.SaveAs(new FileInfo("PersonsExcel.xlsx"));
            }
        }
    }
}