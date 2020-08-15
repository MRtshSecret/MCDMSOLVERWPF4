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

namespace plgTopsis
{
    /// <summary>
    /// Interaction logic for ColumnDetail.xaml
    /// </summary>
    public partial class ColumnDetail : UserControl
    {
        public int Col { get; set; }
        public ColumnDetail()
        {
            InitializeComponent();
            txtNum.Text = _numValue.ToString();
        }

        private int _numValue = 0;

        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue = 1;
        }
        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue = -1;
        }

        private int numval = 0;
        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (int.TryParse(txtNum.Text, out _numValue))
            {
                if (_numValue > 0)
                {
                    txtNum.Text = "1";
                }
                else if (_numValue == 0)
                {
                    txtNum.Text = "1";
                }
                else
                {
                    txtNum.Text = "-1";
                }
            }
            else
            {
                txtNum.Text = "0";
            }
        }

        private void txtWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtWeight.Text, out parsedValue))
            {
                txtWeight.Text = "";
                MessageBox.Show("This is a number only field");
                return;
            }
            else if (Convert.ToDouble(txtWeight.Text) <= 0 && Convert.ToDouble(txtWeight.Text) >= 1)
            {
                txtWeight.Text = "";
            }
            else
            {
            }
        }

        private void txtWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            double _numValues = 0;
            if (!double.TryParse(txtWeight.Text, out _numValues))
            {
                txtWeight.Text = "0";
            }
        }
    }
}
