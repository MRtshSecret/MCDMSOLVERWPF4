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
using System.Windows.Threading;

namespace mcdm.Forms
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {

        //dasdasdahdgkgdakshdgajsghdagd
        public Loading()
        {
            int i = 0;
            InitializeComponent();
        }

        DispatcherTimer timer = new DispatcherTimer();

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }

        private void timer_tick(object sender, EventArgs e)
        {

        }
    }
}
