using AdonisUI.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.IO;



namespace RenameTool_3
{
    public partial class MainWindow : AdonisWindow
    {

        string Excelpath;
        string pdfpath;
        string safepath;
        int cutvalue;
        string datatyp;
        string value1;
        string value2;
        string value3;
        string worksheet;
        string targetfolderpath;
        string customtext;
        List<Renameinfos> excelliste;

        List<string> firstrowlist;
        List<string> worksheetlist;
        string examplestring;
        Excel excelview;
        public MainWindow()
        {
            InitializeComponent();

            int[] cutvalues = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

            for (int i = 0; i < cutvalues.Length; i++)
            {
                cutbox.Items.Add(cutvalues[i]);
            }

            cutbox.SelectedIndex = 0;

            if (Properties.Settings.Default.autosplit == true)
            {
                Splitcheckbox.IsChecked = true;
            }

            string[] datatyps = { ".docx", ".txt", ".mp3", ".pdf" };

            for (int i = 0; i < datatyps.Length; i++)
            {
                Datatypebox.Items.Add(datatyps[i]);
               
            }
            Datatypebox.SelectedIndex = 3;
            datatyp = Datatypebox.SelectedItem?.ToString();



        }

        private void clearvalues()
        {
            value1 = null;
            value2 = null;
            value3 = null;
            worksheet = null;
            Excelpath = null;
            excelliste = null; // Setzen Sie excelliste zurück
            targetfolderpath = null; // Setzen Sie targetfolderpath zurück
            safepath = null; // Setzen Sie safepath zurück
            targetpathbox.Text = "";
            safepathbox.Text = "";
            Value1box.SelectedIndex = -1;
            Value2box.SelectedIndex = -1;
            Worksheetbox.SelectedIndex = -1;

        }

        private void LoadExcel(object sender, RoutedEventArgs e)
        {
            clearvalues();


            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files|*.xlsx;*.xls;*.xlsm";
            dlg.ShowDialog();

            Excelpath = dlg.FileName;

            if (Excelpath == string.Empty)
            {
                return;

            }

            string pathname = dlg.SafeFileName;
            excelpathbox.Text = pathname;
                      


            excelview = new Excel(Excelpath);
            worksheetlist = excelview.Worksheets();
            Worksheetbox.ItemsSource = worksheetlist;

        }

        private void Targetfiles(object sender, RoutedEventArgs e) // Hier werdne die Files angezogen die umbeannt werden, if Splitcheckbox is checked, dann wird das  PDF direkt gesplittet
        {


            if (Splitcheckbox.IsChecked == true && datatyp == ".pdf" ) // Es wird geschauat ob die Checkbox aktiviert ist und ob der Datentyp ein PDF ist
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".pdf";
                dlg.Filter = "PDF Files|*.pdf";
                dlg.ShowDialog();
                pdfpath = dlg.FileName;

                if (pdfpath == string.Empty)
                {
                    return;
                }

          
              
                string targetfoldername = Path.GetFileName(pdfpath);

                targetpathbox.Text = targetfoldername;

            }

            else
            {
                var dlg = new CommonOpenFileDialog
                {
                    IsFolderPicker = true
                };

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {

                    targetfolderpath = dlg.FileName; // Hier erhältst du den vollständigen Pfad

                }

            }

        }


        //private void SplitPDFauto()
        //{
        //    // die Funktion brauche ich in Rename
        //    PdfSplit pdf = new PdfSplit(pdfpath);
        //    targetfolderpath = pdf.Split(cutvalue);
        //}

        private void SplitPDF(object sender, RoutedEventArgs e)
        {


            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Files|*.pdf";
            dlg.ShowDialog();

            if (dlg.FileName == string.Empty)
            {
                AdonisUI.Controls.MessageBox.Show("Bitte wähle ein PDF File aus");
                return;
            }

            else if (cutbox.SelectedItem == null)
            {
                AdonisUI.Controls.MessageBox.Show("Bitte wähle eine Wert für das Splitten");
                return;
            }


            pdfpath = dlg.SafeFileName;



            
            PdfSplit pdf = new PdfSplit(pdfpath);
            pdf.Split(cutvalue);
            AdonisUI.Controls.MessageBoxButton button = AdonisUI.Controls.MessageBoxButton.OK;
            AdonisUI.Controls.MessageBox.Show("Das PDF wurde erfolgreich gesplittet", "Erfolgreich", button);
        }

        private void cutbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cutvalue = (int)cutbox.SelectedItem;
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {

            if (Excelpath == null || Value1box.SelectedItem == null || Value2box.SelectedItem == null || Worksheetbox.SelectedItem == null)
            {
                AdonisUI.Controls.MessageBox.Show("Stelle die Werte ein und wähle eine Excel File");
                return;
            }

            value1 = Value1box.SelectedItem.ToString();
            value2 = Value2box.SelectedItem.ToString();
            value3 = Value3box.SelectedItem?.ToString();
            worksheet = Worksheetbox.SelectedItem.ToString();

            Excel excel = new Excel(Excelpath);
            excelliste = excel.Data(worksheet, value1, value2, value3);
            Excelview excelview = new Excelview(excelliste);
            excelview.Show();


        }

        private void SavePath(object sender, RoutedEventArgs e)
        {
            if (Savepathbutton.Content.ToString() == "reset")
            {
                safepath = null;
                safepathbox.Text = "";
                Savepathbutton.Content = "Browse";
                return;
            }

            var dlg = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {

                safepath = dlg.FileName; // Hier erhältst du den vollständigen Pfad
                safepathbox.Text = safepath;
            }

            if (safepath != null) 
            {
                Savepathbutton.Content = "reset";

            }


        }

        private void RenameButton(object sender, RoutedEventArgs e)
        {

    

            if (Splitcheckbox.IsChecked == true && datatyp == ".pdf") // Es wird geschauat ob die Checkbox aktiviert ist und ob der Datentyp ein PDF ist
            {


                PdfSplit pdf = new PdfSplit(pdfpath);
                try
                {
                    targetfolderpath = pdf.Split(cutvalue);

                }

                catch
                {
                    AdonisUI.Controls.MessageBox.Show("Bitte wähle ein PDF File aus", "Information", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Information);
                    return;
                }
            }

            if (targetfolderpath == null || Excelpath == null || Value1box.SelectedItem == null || Value2box.SelectedItem == null || Worksheetbox.SelectedItem == null)
            {
                AdonisUI.Controls.MessageBox.Show("Bitte fülle alle erforderlichen Felder aus.", "Information", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Information);

                return;
            }

            Excel excel = new Excel(Excelpath);
            excelliste = excel.Data(worksheet, value1, value2, value3); // Excel Liste wird hier mit der Klasse Excel Generiert
            Rename rename = new Rename(targetfolderpath,datatyp, safepath, cutvalue, excelliste);
            try
            {
                rename.RenameFiles(customtext, safepath);

            }
            catch
            {
                AdonisUI.Controls.MessageBox.Show("Es ist ein Fehler aufgetreten", "Information", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Information);
                return;
            }
        }

        private void Splitcheckbox_Checked(object sender, RoutedEventArgs e)
        {

            Properties.Settings.Default.autosplit = true;
            Properties.Settings.Default.Save();

        }

        private void Splitcheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            Properties.Settings.Default.autosplit = false;
            Properties.Settings.Default.Save();


        }

     
        private void Value1box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            value1 = Value1box.SelectedItem?.ToString();
            examplestring = $"{value1} {value2} {value3} {customtext}{datatyp}";

            Example.Text = examplestring;
        }


        private void Value2box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            value2 = Value2box.SelectedItem?.ToString();
            examplestring = $"{value1} {value2} {value3} {customtext}{datatyp}";

            Example.Text = examplestring;

        }

        private void Value3box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            value3 = Value3box.SelectedItem?.ToString();
            examplestring = $"{value1} {value2} {value3} {customtext}{datatyp}";

            Example.Text = examplestring;

        }

        private void Worksheetbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            worksheet = Worksheetbox.SelectedItem?.ToString();
            worksheetlist = excelview.Worksheets();
            firstrowlist = excelview.FirstRow(worksheet); // lädt alles in de ersten Zeile in eine liste

            firstrowlist.Add("");
            worksheetlist.Add(""); // Fügt ein leeres Element hinzu, damit der Benutzer "nichts" auswählen kann


            Value1box.ItemsSource = firstrowlist;

            Value2box.ItemsSource = firstrowlist;
            Value3box.ItemsSource = firstrowlist;


        }

        private void InfoHub(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void SetCustomText()
        {
            customtext = CustomTextbox.Text;

            examplestring = $"{value1} {value2} {value3} {customtext}{datatyp}";
            Example.Text = examplestring;

            if (CustomTextbox.Text.Length > 1)
            {
                SetCustomTextbutton.Content = "Clear";
            }
   

        }


        private void CustomTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            SetCustomText();

        }

        private void SetTextButton(object sender, RoutedEventArgs e)
        {

            SetCustomText();
            if (SetCustomTextbutton.Content.ToString() == "Clear")
            {
                CustomTextbox.Text = "";
                customtext = null;
                SetCustomTextbutton.Content = "Set";
            }
        }

        private void Datatypebox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datatyp = Datatypebox.SelectedItem?.ToString();
            examplestring = $"{value1} {value2} {value3} {customtext}{datatyp}";
            Example.Text = examplestring;
        }

        private void openFolder(object sender, RoutedEventArgs e)
        {
            string folderPath = null;

            if (safepath != null)
            {
                folderPath = safepath;
            }
            else if (targetfolderpath != null)
            {
                // Extrahieren Sie den Ordnerpfad aus dem Dateipfad
                folderPath = Path.GetDirectoryName(targetfolderpath);
            }

            if (folderPath != null)
            {
                Process.Start("explorer.exe", $@"{folderPath}");
            }
            else
            {
                AdonisUI.Controls.MessageBox.Show("Bitte wähle einen Ordner aus", "Information", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Information);
            }
        }
    }
}