using mcdm.Controllers;
using Newtonsoft.Json;
using ProjectMainProp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using System.Windows.Threading;

namespace mcdm.Forms
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {

        //dasdasdahdgkgdakshdgajsghdagd
        int Hide = 0;
        string WTD;
        string KJG = "";
        public int HasAccess = 0;
        public Loading(string wtd, string username = "", string password = "")
        {
            WTD = wtd;
            int i = 0;

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            Console.WriteLine("========================MyWindow_Loaded===========================");
            Console.WriteLine($"{ii} = " + KJG);
            Console.WriteLine("========================MyWindow_Loaded===========================");

            if (WTD == "Login")
            {
                Console.WriteLine("========================MyWindow_Loaded===========================");
                Console.WriteLine($"{ii} = " + KJG);
                Console.WriteLine("========================MyWindow_Loaded===========================");
                WebClient wc = new WebClient();
                string urls = $"{ProjectProp.MasterUrl}/CustomerSideUserApp/userAuth?_u={username}&_p={password}";
                KJG = wc.DownloadString(urls);
                EncDec endc = new EncDec();
                Requests re = JsonConvert.DeserializeObject<Requests>(KJG);
                if (re.RequestEnc == "-1")
                {
                    MessageBox.Show("User Not Found!");
                    Hide = 1;
                    HasAccess = 3;

                }
                else if (re.RequestEnc == "-2")
                {
                    MessageBox.Show("Cannot Connect To The Server");
                    Hide = 1;
                    HasAccess = 2;

                }
                else if (re.Requestname == "OK")
                {
                    UserStruct us = JsonConvert.DeserializeObject<UserStruct>(endc.DecryptText(re.RequestEnc));
                    File.WriteAllText("User.McdM", JsonConvert.SerializeObject(us));
                    HasAccess = 1;
                    Hide = 1;
                }
                ii++;
                Console.WriteLine("========================MyWindow_Loaded===========================");
                Console.WriteLine($"{ii} = " + KJG);
                Console.WriteLine("========================MyWindow_Loaded===========================");

            }
            InitializeComponent();
        }
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {


        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
        int ii = 0;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Hide == 1)
            {
                dispatcherTimer.Stop();
                this.Close();
            }
            Console.WriteLine("========================dispatcherTimer_Tick===========================");

            Console.WriteLine($"{ii} = " + KJG);
            Console.WriteLine("========================dispatcherTimer_Tick===========================");

            ii++;
        }
    }
}
