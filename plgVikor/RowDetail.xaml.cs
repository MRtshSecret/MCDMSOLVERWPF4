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

namespace plgVikor
{
    /// <summary>
    /// Interaction logic for RowDetail.xaml
    /// </summary>
    public partial class RowDetail : UserControl
    {
        public int Row { get; set; }
        public string numberLabel { get; set; }
        public string ReadName { get; set; }
        public RowDetail()
        {
            InitializeComponent();
        }
    }
}
