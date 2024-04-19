using AdonisUI.Controls;
using System.Diagnostics;

namespace RenameTool_3
{
    /// <summary>
    /// Interaktionslogik für Info.xaml
    /// </summary>
    public partial class Info : AdonisWindow
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            // Erstelle ein neues ProcessStartInfo-Objekt mit der URL als Argument
            var psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true // Wichtig, damit der Link im Standardbrowser geöffnet wird
            };

            // Starte den Prozess mit den definierten Startinformationen
            Process.Start(psi);

            // Markiere das Event als behandelt
            e.Handled = true;
        }

    }
}
