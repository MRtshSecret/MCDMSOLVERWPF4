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
        Window algoWindowTest = new Window();
        Grid parentGrid = new Grid();
        AlgorithmWindow algWindow;

        public newProject(Grid Gparent)
        {
            InitializeComponent();
            parentGrid = Gparent;
            //Code Zir Nemune Dadam Harchand Baladi
            algWindow = new AlgorithmWindow(algoWindowTest);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Inja Bayad panele mother az usercontrol algWindow Ro Ba PlgTopsis Ya Harchi Dg Por Koni!
            parentGrid.Children.Clear();
            parentGrid.Children.Add(algWindow);
        }
    }
}
