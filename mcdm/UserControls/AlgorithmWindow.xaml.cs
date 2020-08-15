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

namespace mcdm.UserControls
{
    /// <summary>
    /// Interaction logic for AlgorithmWindow.xaml
    /// </summary>
    public partial class AlgorithmWindow : UserControl
    {
        //------------------------------------>>>>/  "algoWindow" Ro Migire Bad Poresh Kon!

        public AlgorithmWindow(UserControl algoWindow)
        {

            InitializeComponent();
            mother.Children.Clear();
            this.mother.Children.Add(algoWindow);
        }








    }
}
