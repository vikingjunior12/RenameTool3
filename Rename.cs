using AdonisUI.Controls;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

// Todo, Rename Button muss alles übernehmen, auch das PDF Splitten (Bei Auto SPlit Aktiv)

namespace RenameTool_3
{
    public class Rename
    {
        string targetfolderpath { get; set; }
        
        string datatyp { get; set; }
        string safepath { get; set; }
        int cutvalue { get; set; }
        List<Renameinfos> liste { get; set; }
        string neuerOrdnerPfad; // Neuer Ordner für die Dateien
        string neuerPfad; // Neuer Pfad für die Datei
        string[] pdfFiles; // Array für die PDF Dateien



        public Rename(string targetfolderpath, string datatyp, string safepath, int cutvalue, List<Renameinfos> liste)
        {
            this.targetfolderpath = targetfolderpath;
            this.datatyp = datatyp;
            this.safepath = safepath;
            this.cutvalue = cutvalue;
            this.liste = liste; // Zuweisung der übergebenen Liste zur Eigenschaft 'liste'

        }

        //public string Foldercheck(string Folder)
        //{
        //    try
        //    {
        //        if (Directory.Exists(Folder))
        //        {
        //            Directory.Delete(Folder, true);
        //        }
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        MessageBox.Show($"Fehler beim Löschen des Ordners: {ex.Message}");
                
        //    }

        //    try
        //    {
        //        Directory.CreateDirectory(Folder);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        MessageBox.Show($"Fehler beim Erstellen des Ordners: {ex.Message}");
               
        //    }

        //    return Folder;
        //}


        public void OpenFolderInExplorer(string Folder)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Folder,
                UseShellExecute = true // Benötigt, damit der Ordner im Explorer geöffnet wird, weil default ist "False"
            });
        }


        public void RenameFiles(string customtext = default, string safepath = default)
        {

        
            try { pdfFiles = Directory.GetFiles(targetfolderpath, $"*{datatyp}"); }

            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Lesen der Dateien: {ex.Message}");
                return;
            }
            var sortedPdfFiles = pdfFiles.OrderBy(file => Regex.Replace(Path.GetFileName(file), @"\d+", match => match.Value.PadLeft(20, '0'))).ToArray();
            // Regex ziemlich geil und mächtig, da wird es einfach sauber sortiert, damit es zur Liste passt
            string filename = "RenamedFiles"; // Name für Ordner wenn der SafePath gesetzt ist


            if (safepath != default)
            {
                neuerOrdnerPfad = Path.Combine(safepath, filename);


                try
                {
                    if (!Directory.Exists(neuerOrdnerPfad))
                    {
                        Directory.CreateDirectory(neuerOrdnerPfad);
                    }
                    if (neuerOrdnerPfad == null)
                    {
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Erstellen des Ordners: {ex.Message}");
                }
            }

         

            for (int i = 0; i < liste.Count && i < sortedPdfFiles.Length; i++) // Stellt sicher, dass die Schleife nicht über das Array hinausgeht
            {
                if (customtext == default)
                {


                    var neuerName = liste[i].value3 != null ? $"{liste[i].value1} {liste[i].value2} {liste[i].value3}{datatyp}" : $"{liste[i].value1} {liste[i].value2}{datatyp}";
                    
                   if (safepath != default)
                    {

                        try
                        {
                            neuerPfad = Path.Combine(neuerOrdnerPfad, neuerName);
                        }
                        catch
                        {
                            //MessageBox.Show($"Fehler beim Erstellen des Ordners: {ex.Message}");
                        }
                       

                    }
                    else
                    {   
                        
                        neuerPfad = Path.Combine(targetfolderpath, neuerName);
                    }
                   
                    try
                    {
                        File.Move(sortedPdfFiles[i], neuerPfad); // Versucht, die Datei umzubenennen
                    }
                    catch
                    {
                        //MessageBox.Show($"Fehler beim Umbenennen der Datei: {ex.Message}");
                    }


                }

                else // Wenn customtext nicht default ist, sondern gesetzt wurde, mit Customtext
                {
              
                    var neuerName = liste[i].value3 != null ? $"{liste[i].value1} {liste[i].value2} {liste[i].value3} {customtext}{datatyp}" : $"{liste[i].value1} {liste[i].value2} {customtext}{datatyp}";
                    if (safepath != default)
                    {


                        try
                        {
                            neuerPfad = Path.Combine(neuerOrdnerPfad, neuerName);
                        }
                        catch
                        {
                            //MessageBox.Show($"Fehler beim Erstellen des Ordners: {ex.Message}");
                        }
                    }
                    else
                    {
                        
                        neuerPfad = Path.Combine(targetfolderpath, neuerName);
                    }

                    try
                    {
                        File.Move(sortedPdfFiles[i], neuerPfad); // Versucht, die Datei umzubenennen

                    }
                    catch
                    {
                       // MessageBox.Show($"Fehler beim Umbenennen der Datei: {ex.Message}");
                    }


                }
                
            }
            MessageBox.Show("Die Files wurden unbennant, möchtest du den Ordner gleich öffnen?", "Info", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (MessageBoxResult.OK == MessageBoxResult.OK)
            {
                try
                {
                    if (safepath != default)
                    {
                        OpenFolderInExplorer(neuerOrdnerPfad);
                    }
                    else
                    { OpenFolderInExplorer(targetfolderpath);}
                 

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Öffnen des Ordners: {ex.Message}");
                }
            }
        }
        


    }


}


