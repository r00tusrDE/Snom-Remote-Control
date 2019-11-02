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
using System.Windows.Shapes;

namespace Snom_Remote_Control
{
    /// <summary>
    /// Interaktionslogik für FrmSettings.xaml
    /// </summary>
    public partial class FrmSettings : Window
    {
        private bool settingsChanged = false;
        private bool loadingCompleted = false;

        public FrmSettings()
        {
            InitializeComponent();
            LoadSettings();
            loadingCompleted = true;
        }

        private bool IpIsValid(string ip)
        {
            return Regex.IsMatch(ip, @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
        }

        private void IpInvalidWarning()
        {
            MessageBox.Show("Ungültige IP", "Ungültige IP-Adresse angegeben. Bitte korrigieren!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IpIsValid(tbIPaddress.Text))
            {
                SaveSettings();
                this.Close();
            }
            else
            {
                IpInvalidWarning();
            }
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            if (IpIsValid(tbIPaddress.Text))
            {
                SaveSettings();
            }
            else
            {
                IpInvalidWarning();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadSettings()
        {
            tbIPaddress.Text = Settings.Default.PhoneIP;
            cbAuth.IsChecked = Settings.Default.LoginEnabled;
            cbSwitch.SelectedIndex = Settings.Default.AutoSwitchOutput;
        }

        private void SaveSettings()
        {
            Settings.Default.PhoneIP = tbIPaddress.Text;
            Settings.Default.Username = tbUsername.Text;
            Settings.Default.Password = tbPassword.SecurePassword.ToString();

            Settings.Default.LoginEnabled = (bool)cbAuth.IsChecked;

            Settings.Default.AutoSwitchOutput = (byte)cbSwitch.SelectedIndex;

            Settings.Default.Save();
            MessageBox.Show("Settings saved....");
            settingsChanged = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (settingsChanged && MessageBox.Show("Sollen die Änderungen gespeichert werden?", "Änderungen wurde vorgenommen!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                SaveSettings();
            }
        }

        private void Settings_Changed(object sender, RoutedEventArgs e)
        {
            if (loadingCompleted)
            {
                settingsChanged = true;
                btnApply.IsEnabled = true;
                btnSave.IsEnabled = true;
            }
        }

        private void Settings_Changed(object sender, TextChangedEventArgs e)
        {
            if (loadingCompleted)
            {
                settingsChanged = true;
                btnApply.IsEnabled = true;
                btnSave.IsEnabled = true;
            }
        }
    }
}
