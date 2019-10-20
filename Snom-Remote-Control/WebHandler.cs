using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Snom_Remote_Control
{
    public static class WebHandler
    {
        public static async void SendKeyAsync(string key)
        {
            if (Properties.Settings.Default.LoginEnabled)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.Username + ":" + Properties.Settings.Default.Password + "@" + Properties.Settings.Default.PhoneIP + "/command.htm?key=" + key);
                await request.GetResponseAsync();
                request.Abort();
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.PhoneIP + "/command.htm?key=" + key);
                await request.GetResponseAsync();
                request.Abort();
            }
        }

        public static async void SendCmdAsync(string cmd)
        {
            if (Properties.Settings.Default.LoginEnabled)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.Username + ":" + Properties.Settings.Default.Password + "@" + Properties.Settings.Default.PhoneIP + "/command.htm?" + cmd);
                await request.GetResponseAsync();
                request.Abort();
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Properties.Settings.Default.PhoneIP + "/command.htm?" + cmd);
                await request.GetResponseAsync();
                request.Abort();
            }
        }

        public static async void RestartPhone()
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
    }
}
