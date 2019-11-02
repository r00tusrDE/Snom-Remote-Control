using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Snom_Remote_Control
{
    public static class WebHandler
    {
        public static async void SendKeyAsync(string key)
        {
            if (Properties.Settings.Default.LoginEnabled)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.Username + ":" + Properties.Settings.Default.Password + "@" + Properties.Settings.Default.PhoneIP + "/command.htm?key=" + key);
                    await request.GetResponseAsync();
                    request.Abort();
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.PhoneIP + "/command.htm?key=" + key);
                    await request.GetResponseAsync();
                    request.Abort();
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static async void SendCmdAsync(string cmd)
        {
            if (Properties.Settings.Default.LoginEnabled)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.Username + ":" + Properties.Settings.Default.Password + "@" + Properties.Settings.Default.PhoneIP + "/command.htm?" + cmd);
                    await request.GetResponseAsync();
                    request.Abort();
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.PhoneIP + "/command.htm?" + cmd);
                    await request.GetResponseAsync();
                    request.Abort();
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static async void RestartPhone()
        {
            try
            {
                if (Properties.Settings.Default.LoginEnabled)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.Username + ":" + Properties.Settings.Default.Password + "@" + Properties.Settings.Default.PhoneIP + "/advanced_update.htm?reboot=Reboot");
                    await request.GetResponseAsync();
                    request.Abort();
                }
                else
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.PhoneIP + "/advanced_update.htm?reboot=Reboot");
                    await request.GetResponseAsync();
                    request.Abort();
                }
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void HandleWebException(WebException ex)
        {
            if (ex.InnerException is null)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
