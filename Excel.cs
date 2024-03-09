using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RenameTool_3
{
    class Excel
    {
        string path { get; set; }


        public Excel(string path)
        {
            this.path = path;
        }

        public List<string> FirstRow()
        {
            using (var workbook = new XLWorkbook(path))
            {
                var worksheet = workbook.Worksheet(1);

                var firstRowUsed = worksheet.FirstRowUsed();

                var firstrowlist = new List<string>();
                foreach (var cell in firstRowUsed.CellsUsed())
                {
                    firstrowlist.Add(cell.Value.ToString());
                }

                return firstrowlist;


            }

        }


        public List<string> Worksheets()
        {
            var Workbook = new XLWorkbook(path);
            var Worksheetlist = new List<string>();
            foreach (var worksheet in Workbook.Worksheets)
            {
                Worksheetlist.Add(worksheet.Name);
            }
            return Worksheetlist;
        }


        public List<string> Data(string worksheetName, string value1, string value2, string value3)
        {
            var dataList = new List<string>();

            using (var workbook = new XLWorkbook(path))
            {
                var worksheet = workbook.Worksheet(worksheetName);
                var firstRow = worksheet.FirstRowUsed();

                // Finden Sie die Spaltennamen, die den angegebenen Werten entsprechen
                string column1 = null, column2 = null, column3 = null;
                foreach (var cell in firstRow.CellsUsed())
                {
                    if (cell.Value.ToString() == value1)
                        column1 = cell.Address.ColumnLetter;

                    if (cell.Value.ToString() == value2)
                        column2 = cell.Address.ColumnLetter;

                    if (cell.Value.ToString() == value3)
                        column3 = cell.Address.ColumnLetter;
                }

                // Überprüfen Sie, ob alle Spalten gefunden wurden
                if (column1 == null || column2 == null || column3 == null)
                    return dataList; // oder werfen Sie eine Ausnahme

                // Durchlaufen Sie die restlichen Zeilen und extrahieren Sie die Daten
                foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip(1) um die Kopfzeile zu überspringen
                {
                    var cell1 = row.Cell(column1);
                    var cell2 = row.Cell(column2);
                    var cell3 = row.Cell(column3);

                    if (cell1.Value.ToString() != "" && cell2.Value.ToString() != "" && cell3.Value.ToString() != "")
                    {
                        dataList.Add($"{cell1.Value}, {cell2.Value}, {cell3.Value}");
                    }
                }
            }

            return dataList;
        }



    }
}
