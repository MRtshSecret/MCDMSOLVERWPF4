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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class newProject : UserControl
    {
        UserControl algoWindowTest = new UserControl();
        Grid parentGrid = new Grid();
        AlgorithmWindow algWindow;

        public newProject(Grid Gparent)
        {
            InitializeComponent();
            parentGrid = Gparent;
            clear();
            //Code Zir Nemune Dadam Harchand Baladi
            //algWindow = new AlgorithmWindow(algoWindowTest);
        }
        string WhichAlg = "";
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtprojectname.Text))
            {
                switch (WhichAlg)
                {
                    case "Topsis":
                        parentGrid.Children.Clear();
                        plgTopsis.plgTopsisWindow Child = new plgTopsis.plgTopsisWindow();
                        parentGrid.Children.Add(Child);
                        break;
                    case "SAW":
                        parentGrid.Children.Clear();
                        plgSAW.plgSAWWindow Child3 = new plgSAW.plgSAWWindow();
                        parentGrid.Children.Add(Child3);

                        break;
                    //case "Vikor":
                    //    parentGrid.Children.Clear();
                    //    plgVikor.plgVikorWindow Child2 = new plgVikor.plgVikorWindow();
                    //    parentGrid.Children.Add(Child2);
                    //    break;
                    //case "Regim":
                    //    parentGrid.Children.Clear();
                    //    plgRegim.plgRegimWindow Child1 = new plgRegim.plgRegimWindow();
                    //    parentGrid.Children.Add(Child1);
                    //    break;
                    default:

                        break;
                }
            }
            else
            {
                MessageBox.Show("ProjectName Required !");
            }
        }
        void clear()
        {
            imgTopsis.Visibility = Visibility.Hidden;
            imgV1.Visibility = Visibility.Hidden;
            imgV2.Visibility = Visibility.Hidden;
            imgV3.Visibility = Visibility.Hidden;
        }
        private void btnNewTopsis_Click(object sender, RoutedEventArgs e)
        {
            clear();
            WhichAlg = "Topsis";
            imgTopsis.Visibility = Visibility.Visible;

        }

        private void btnV1_Click(object sender, RoutedEventArgs e)
        {
            clear();
            WhichAlg = "SAW";
            imgV1.Visibility = Visibility.Visible;
        }

        private void btnV2_Click(object sender, RoutedEventArgs e)
        {
            clear();
            WhichAlg = "Vikor";
            imgV2.Visibility = Visibility.Visible;
        }

        private void btnV3_Click(object sender, RoutedEventArgs e)
        {
            clear();
            WhichAlg = "Regim";
            imgV3.Visibility = Visibility.Visible;
        }
    }
}
