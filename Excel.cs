using ClosedXML.Excel;


namespace RenameTool_3
{
    class Excel
    {
        string path { get; set; }
    


        public Excel(string path)
        {
            this.path = path;
        }

        public List<string> FirstRow(string worksheetvalue = default)
        {
            var worksheet = default(IXLWorksheet);
            using (var workbook = new XLWorkbook(path))
            {
              
                // eror handling noch einbauen fallsm Workssheet nicht existiert
                if (worksheetvalue == default)
                {
                     worksheet = workbook.Worksheet(1);


                }
                else
                {
                     worksheet = workbook.Worksheet(worksheetvalue);
                }

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


        public List<Renameinfos> Data(string worksheetName, string value1, string value2, string value3 = default, string customtext = default)
        {
            List<Renameinfos> renameInfosList = new List<Renameinfos>();

            using (var workbook = new XLWorkbook(path))
            {
                var worksheet = workbook.Worksheet(worksheetName);
                var firstRow = worksheet.FirstRowUsed();
                var firstCell = firstRow.FirstCellUsed();
                var value_1 = worksheet.FirstRow().CellsUsed().First(c => c.Value.ToString() == value1).WorksheetColumn().ColumnNumber();
                var value_2 = worksheet.FirstRow().CellsUsed().First(c => c.Value.ToString() == value2).WorksheetColumn().ColumnNumber();
                var value_3 = value3 != default ? worksheet.FirstRow().CellsUsed().First(c => c.Value.ToString() == value3).WorksheetColumn().ColumnNumber() : default;

                //Value 3 ist freiwilig, eingbautes IF, quasi if nicht default dann

                foreach (var row in worksheet.RowsUsed())
                {
                    var zelle = row.Cell(value_1);
                    var zelle2 = row.Cell(value_2);
                    var zelle3 = value3 != default ? row.Cell(value_3) : default;
                    var customtextvalue = customtext != default ? customtext : default;

                    if (zelle.GetValue<string>() != value1 && (zelle2.GetValue<string>() != value2)) // Überspringen der Überschrift
                    {
                        // Erstellen Sie ein neues Renameinfos-Objekt und fügen Sie es zur Liste hinzu
                        renameInfosList.Add(
                            new Renameinfos(zelle.GetValue<string>(),
                            zelle2.GetValue<string>(),
                            zelle3?.GetValue<string>()

                            ));

                    }


                }
            }

            return renameInfosList;
        }




    }
}
