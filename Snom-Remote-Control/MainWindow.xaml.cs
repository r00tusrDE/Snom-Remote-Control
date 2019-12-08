using Snom_Remote_Control.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadWindowSettings();
        }

        private void BtnCall_Click(object sender, RoutedEventArgs e)
        {
            string callNumber = tbNumber.Text;

            // Only allow 0-9, +, -, /, *, (, )
            if (new Regex(@"[0-9+\-\/\*\(\)]").Matches(callNumber).Count == 0)
            {
                MessageBox.Show("Bitte trage eine gültige Telefonnummer in das Textfeld ein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Remove spaces, slashes and dashes from number
                callNumber = Regex.Replace(callNumber, @"\s+|\/|\-|\(|\)", "");

                if (callNumber.StartsWith("+"))
                    callNumber = Regex.Replace(callNumber, @"\+", "00");

                if (callNumber.Substring(4, 1).Equals("0"))
                    callNumber = callNumber.Remove(4, 1);

                switch (Settings.Default.AutoSwitchOutput)
                {
                    case 1:
                        WebHandler.SendCmdAsync("number=" + callNumber);
                        WebHandler.SendKeyAsync("HEADSET");
                        break;
                    case 2:
                        WebHandler.SendCmdAsync("number=" + callNumber);
                        WebHandler.SendKeyAsync("SPEAKER");
                        break;
                    default:
                        WebHandler.SendCmdAsync("number=" + callNumber);
                        break;
                }
            }

        }

        private void BtnPickUp_Click(object sender, RoutedEventArgs e)
        {
            switch (Settings.Default.AutoSwitchOutput)
            {
                case 1:
                    WebHandler.SendKeyAsync("HEADSET");
                    break;
                case 2:
                    WebHandler.SendKeyAsync("SPEAKER");
                    break;
                default:
                    WebHandler.SendKeyAsync("OFFHOOK");
                    break;
            }
        }

        private void BtnEndCall_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("ONHOOK");
            WebHandler.SendKeyAsync("CANCEL");
            if (Settings.Default.AutoSwitchOutput == 1)
            {
                WebHandler.SendKeyAsync("HEADSET");
            }
        }

        private void BtnDND_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("DND");
        }

        private void BtnTransfer_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("TRANSFER");
        }

        private void BtnHold_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("F_HOLD");
        }

        private void BtnVolumeDown_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("VOLUME_DOWN");
        }

        private void BtnVolumeUp_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("VOLUME_UP");
        }

        private void BtnSpeaker_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("SPEAKER");
        }

        private void BtnHeadset_Click(object sender, RoutedEventArgs e)
        {
            WebHandler.SendKeyAsync("HEADSET");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                Settings.Default.WindowHeight = this.Height;
                Settings.Default.WindowWidth = this.Width;
                Settings.Default.WindowTop = this.Top;
                Settings.Default.WindowLeft = this.Left;
            }
            else
            {
                Settings.Default.WindowHeight = this.RestoreBounds.Height;
                Settings.Default.WindowWidth = this.RestoreBounds.Width;
                Settings.Default.WindowTop = this.RestoreBounds.Top;
                Settings.Default.WindowLeft = this.RestoreBounds.Left;
            }
            Settings.Default.Save();
        }

        private void LoadWindowSettings()
        {
            //this.Height = Settings.Default.WindowHeight;
            //this.Width = Settings.Default.WindowWidth;
            this.Top = Settings.Default.WindowTop;
            this.Left = Settings.Default.WindowLeft;
        }

        private void BtnNumpad_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            tbNumber.Text += btn.Content.ToString();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.Owner = this;
            frmAbout.ShowDialog();
        }

        private void MenuItemSettings_Click(object sender, RoutedEventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Owner = this;
            frmSettings.ShowDialog();
        }

        private void MenuItemKBControl_Click(object sender, RoutedEventArgs e)
        {
            FrmKeyboardControl frmKeyboardControl = new FrmKeyboardControl();
            frmKeyboardControl.Show();
        }

        private void menuItemRestartPhone_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll das Telefon wirklich neu gestartet werden?", "Telefon Neustart", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                WebHandler.RestartPhone();
            }
        }

        private void tbNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnCall_Click(sender, e);
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
