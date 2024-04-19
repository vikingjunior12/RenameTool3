using AdonisUI.Controls;

namespace RenameTool_3
{
    /// <summary>
    /// Interaktionslogik für Excelview.xaml
    /// </summary>
    public partial class Excelview : AdonisWindow
    {   
        List<Renameinfos> liste { get; set;}
        public Excelview(List<Renameinfos> liste)
        {
                   
           InitializeComponent();
           Excelliste.ItemsSource = liste;
           
        }
    }
}
