using Snom_Remote_Control.Properties;
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

namespace Snom_Remote_Control
{
    /// <summary>
    /// Interaktionslogik für FrmKeyboardControl.xaml
    /// </summary>
    public partial class FrmKeyboardControl : Window
    {
        public FrmKeyboardControl()
        {
            InitializeComponent();
            LoadWindowSettings();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    WebHandler.SendKeyAsync("UP");
                    break;
                case Key.Down:
                    WebHandler.SendKeyAsync("DOWN");
                    break;
                case Key.Right:
                    WebHandler.SendKeyAsync("RIGHT");
                    break;
                case Key.Left:
                    WebHandler.SendKeyAsync("LEFT");
                    break;
                case Key.M:
                    WebHandler.SendKeyAsync("MENU");
                    break;
                case Key.Enter:
                    WebHandler.SendKeyAsync("ENTER");
                    break;
                case Key.Escape:
                    WebHandler.SendKeyAsync("CANCEL");
                    break;
                case Key.D:
                    WebHandler.SendKeyAsync("DND");
                    break;
                case Key.W:
                    WebHandler.SendKeyAsync("REDIAL");
                    break;
                case Key.H:
                    WebHandler.SendKeyAsync("HEADSET");
                    break;
                case Key.F1:
                    WebHandler.SendKeyAsync("F1");
                    break;
                case Key.F2:
                    WebHandler.SendKeyAsync("F2");
                    break;
                case Key.F3:
                    WebHandler.SendKeyAsync("F3");
                    break;
                case Key.F4:
                    WebHandler.SendKeyAsync("F4");
                    break;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                Settings.Default.KBCWindowHeight = this.Height;
                Settings.Default.KBCWindowWidth = this.Width;
                Settings.Default.KBCWindowTop = this.Top;
                Settings.Default.KBCWindowLeft = this.Left;
            }
            else
            {
                Settings.Default.KBCWindowHeight = this.RestoreBounds.Height;
                Settings.Default.KBCWindowWidth = this.RestoreBounds.Width;
                Settings.Default.KBCWindowTop = this.RestoreBounds.Top;
                Settings.Default.KBCWindowLeft = this.RestoreBounds.Left;
            }
            Settings.Default.Save();
        }

        private void LoadWindowSettings()
        {
            this.Height = Settings.Default.KBCWindowHeight;
            this.Width = Settings.Default.KBCWindowWidth;
            this.Top = Settings.Default.KBCWindowTop;
            this.Left = Settings.Default.KBCWindowLeft;
        }
    }
}
