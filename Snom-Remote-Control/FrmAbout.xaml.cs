using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snom_Remote_Control
{
    /// <summary>
    /// Interaktionslogik für FrmAbout.xaml
    /// </summary>
    public partial class FrmAbout : Window
    {
        private string licenseUrl = "https://git.dopend.de/r00tusr/Snom-Remote-Control/src/branch/master/LICENSE";
        private string sourceCodeUrl = "https://git.dopend.de/r00tusr/Snom-Remote-Control";

        public FrmAbout()
        {
            InitializeComponent();
            lblVersion.Text = FileVersionInfo.GetVersionInfo(Application.ResourceAssembly.Location).ProductVersion;
            hlLicense.NavigateUri = new Uri(licenseUrl);
            hlLicense.ToolTip = licenseUrl;
            hlSourceCode.NavigateUri = new Uri(sourceCodeUrl);
            hlSourceCode.ToolTip = sourceCodeUrl;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
