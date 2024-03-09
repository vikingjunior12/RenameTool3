using AdonisUI.Controls;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RenameTool_3
{
    /// <summary>
    /// Interaktionslogik für Excelview.xaml
    /// </summary>
    public partial class Excelview : AdonisWindow
    {   
        List<string> liste { get; set;}
        public Excelview(List<string> liste)
        {
           
         
           InitializeComponent();

           Excelliste.ItemsSource = liste;
        }
    }
}
