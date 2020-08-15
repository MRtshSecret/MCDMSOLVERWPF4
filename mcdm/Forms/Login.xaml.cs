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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace mcdm.Forms
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        bool HiddenPassword = true;
        public Login()
        {
            InitializeComponent();
        }

        private void btnHidePassword_Click(object sender, RoutedEventArgs e)
        {
            if (HiddenPassword == true)
            {
                HiddenPassword = false;
                txtPassword.Text = pboxPassword.Password;
                pboxPassword.Visibility = Visibility.Collapsed;
                txtPassword.Visibility = Visibility.Visible;
                Uri resourceUri = new Uri("/Assets/visible.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                var brush = new ImageBrush();
                brush.ImageSource = temp;

                btnHidePassword.Background = brush;
            }
            else
            {
                HiddenPassword = true;
                pboxPassword.Password = txtPassword.Text;
                pboxPassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Collapsed;
                Uri resourceUri = new Uri("/Assets/hide.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                var brush = new ImageBrush();
                brush.ImageSource = temp;

                btnHidePassword.Background = brush;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void btnsUBMIT_Click(object sender, RoutedEventArgs e)
        {
            btnsUBMIT.IsEnabled = false;
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(pboxPassword.Password) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                var pgload = new Forms.Loading("Login", txtUsername.Text, pboxPassword.Password);
                pgload.ShowDialog();
                if(pgload.HasAccess == 1)
                {
                    MessageBox.Show("Welcome");
                    this.Hide();
                    MainWindow ww = new MainWindow();
                    ww.Show();
                }
                else
                {
                    btnsUBMIT.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Username and Password Are Important");
                btnsUBMIT.IsEnabled = true;
            }
        }
    }
}
