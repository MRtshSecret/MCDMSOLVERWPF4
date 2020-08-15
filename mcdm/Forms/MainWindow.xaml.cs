using mcdm.UserControls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mcdm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Dashboard dashboard = new Dashboard();
        newProject newProject;
        loadProject loadProject = new loadProject();

        public MainWindow()
        {
            var pgload = new Forms.Loading("Login");
            pgload.ShowDialog();
            newProject = new newProject(pnl_Main);
            Loaded += MyWindow_Loaded;
            InitializeComponent();
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //set tooltip visibility
            if (Tg_Btn.IsChecked == true)
            {
                tt_dashboard.Visibility = Visibility.Collapsed;
                tt_new.Visibility = Visibility.Collapsed;
                tt_load.Visibility = Visibility.Collapsed;
                tt_shop.Visibility = Visibility.Collapsed;
                tt_bug_report.Visibility = Visibility.Collapsed;
                tt_heart.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_dashboard.Visibility = Visibility.Visible;
                tt_new.Visibility = Visibility.Visible;
                tt_load.Visibility = Visibility.Visible;
                tt_shop.Visibility = Visibility.Visible;
                tt_bug_report.Visibility = Visibility.Visible;
                tt_heart.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void topPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
            BG.Opacity = 0.3;
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
            BG.Opacity = 1;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void Home_Selected(object sender, RoutedEventArgs e)
        {
            pnl_Main.Children.Clear();
            pnl_Main.Children.Add(dashboard);
        }

        private void new_Selected(object sender, RoutedEventArgs e)
        {
            pnl_Main.Children.Clear();
            pnl_Main.Children.Add(newProject);
        }

        private void load_Selected(object sender, RoutedEventArgs e)
        {
            pnl_Main.Children.Clear();
            pnl_Main.Children.Add(loadProject);
        }
    }
}
