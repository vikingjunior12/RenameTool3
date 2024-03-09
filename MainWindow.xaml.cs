using AdonisUI.Controls;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;



namespace RenameTool_3
{
   
    public partial class MainWindow: AdonisWindow
    {

        string Excelpath;
        string pdfpath;
        int cutvalue;

        public MainWindow()
        {
            InitializeComponent();
            int[] array2 = [1, 2, 3, 4, 5, 6,7,8,9,10];
            
            for (int i = 0; i < array2.Length; i++)
            {
                cutbox.Items.Add(array2[i]);
            }
          

         
        }

        private void LoadExcel(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files|*.xlsx;*.xls";
            dlg.ShowDialog();

            Excelpath = dlg.FileName;
            excelpathbox.Text = Excelpath;

            Excel excelview = new Excel(Excelpath);
            List<string> liste = excelview.FirstRow();
            List<string> worksheetlist = excelview.Worksheets();

            Value1box.ItemsSource = liste;
            Value2box.ItemsSource = liste;
            Value3box.ItemsSource = liste;
            Worksheetbox.ItemsSource = worksheetlist;
      

        }

        private void Targetfiles(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFolderDialog dlg = new Microsoft.Win32.OpenFolderDialog();
            dlg.Title = "Select a folder";
            dlg.InitialDirectory = @"C:\";
            dlg.ShowDialog();

            string folderpath = dlg.SafeFolderName;
            targetpathbox.Text = folderpath;



        }

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


            pdfpath = dlg.FileName;


 

            PdfSplit pdf = new PdfSplit(pdfpath);
            pdf.Split(cutvalue);

        }

        private void cutbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cutvalue = (int)cutbox.SelectedItem;
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {

            if (Excelpath == null)
            {
                AdonisUI.Controls.MessageBox.Show("Bitte wähle eine Excel Datei aus");
                return;
            }

            var value1 = Value1box.SelectedItem.ToString();
            var value2 = Value2box.SelectedItem.ToString(); 
            var value3 = Value3box.SelectedItem.ToString();
            var worksheet = Worksheetbox.SelectedItem.ToString();

            Excel excel = new Excel(Excelpath);
            List<string> liste = excel.Data(worksheet, value1,value2,value3);
            Excelview excelview = new Excelview(liste);
            excelview.Show();

        }
    }   
}