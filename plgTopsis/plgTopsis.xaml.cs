using plgTopsis.MainItems;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class plgTopsisWindow : UserControl
    {
        int Columns, Rows;
        DataTable dtStep3 = new DataTable();
        DataTable dtStep4 = new DataTable();
        List<int> inputSign = new List<int>();
        List<double> inputWeight = new List<double>();
        public string foldername = "";

        public plgTopsisWindow()
        {
            InitializeComponent();
        }

        private void CBBRowCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Step 1 
            pnlRowDetails.Children.Clear();
            //Step 2
            ComboBoxItem typeItem = (ComboBoxItem)CBBRowCount.SelectedItem;
            string value = typeItem.Content.ToString();
            Rows = Convert.ToInt32(value);
            for (int i = 1; i <= Rows; i++)
            {
                RowDetail generated = new RowDetail();
                generated.Row = i;
                generated.Name = "row_" + i;
                generated.Margin = new Thickness(20);
                generated.HorizontalAlignment = HorizontalAlignment.Stretch;
                generated.Tag = "AllRows";
                pnlRowDetails.Children.Add(generated);
            }
        }

        private void CBBColCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Step 1 
            pnlColDetails.Children.Clear();
            //Step 2
            ComboBoxItem typeItem = (ComboBoxItem)CBBColCount.SelectedItem;
            string value = typeItem.Content.ToString();
            Columns = Convert.ToInt32(value);
            for (int i = 1; i <= Columns; i++)
            {
                ColumnDetail generated = new ColumnDetail();
                generated.Col = i;
                generated.Name = "column_" + i;
                generated.Margin = new Thickness(20);
                generated.Tag = "AllColumns";
                generated.HorizontalAlignment = HorizontalAlignment.Stretch;
                pnlColDetails.Children.Add(generated);
            }
        }

        // ------------------------------------------------------ Previous Steps Buttons
        #region Next Steps
        private void btnNextStep1_Click(object sender, RoutedEventArgs e)
        {
            string tagName = "AllColumns";
            var foundButton = pnlColDetails.Children.OfType<ColumnDetail>().Where(x => x.Tag.ToString() == tagName).ToList<ColumnDetail>();
            CheckItemsCols cic = new CheckItemsCols(foundButton);
            if (cic.WeightSum())
            {
                if (cic.ISAllNameFilled())
                {
                    //Disable Other Tabs
                    if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                        return; // No more tabs to show!
                    var currentTab = TabControlMain.SelectedItem as TabItem;
                    currentTab.IsEnabled = false;
                    var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex + 1] as TabItem;
                    nextTab.IsEnabled = true;
                    TabControlMain.SelectedItem = nextTab;
                    //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
                }
                else
                {
                    MessageBox.Show("All Name Fields Required!");
                }
            }
            else
            {
                MessageBox.Show("Sum of weights Should be  1.00");
            }
        }

        private void btnNextStep2_Click(object sender, RoutedEventArgs e)
        {
            string tagName = "AllRows";
            var foundButton = pnlRowDetails.Children.OfType<RowDetail>().Where(x => x.Tag.ToString() == tagName).ToList<RowDetail>();
            CheckItemsRows cir = new CheckItemsRows(foundButton);
            if (cir.ISAllNameFilled())
            {
                dtStep3.Columns.Add("*");
                foreach (ColumnDetail item in pnlColDetails.Children)
                {
                    //dtStep3.Columns.Add(item.txtBoxColName.Text);
                    dtStep3.Columns.Add(item.txtName.Text);
                    inputWeight.Add(Convert.ToDouble(item.txtWeight.Text));
                    inputSign.Add(item.NumValue);
                }

                foreach (RowDetail item in pnlRowDetails.Children)
                {
                    DataRow newRow = dtStep3.NewRow();
                    newRow[0] = item.txtName.Text;

                    dtStep3.Rows.Add(newRow);
                }
                int aa = dgvStep3.Columns.Count;

                dgvStep3.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dtStep3 });
                for (int i = 0; i < dgvStep3.Items.Count; i++)
                {
                    //dtgStep3.Items[i].Cells[1].ReadOnly = true;
                    //dtgStep3.Rows[i].Cells[1].Style.BackColor = Color.Gray;
                }
                dgvStep3.Items.Refresh();

                //Disable Other Tabs
                if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                    return; // No more tabs to show!

                var currentTab = TabControlMain.SelectedItem as TabItem;
                currentTab.IsEnabled = false;
                var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex + 1] as TabItem;
                nextTab.IsEnabled = true;
                TabControlMain.SelectedItem = nextTab;
                //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
            }
            else
            {
                MessageBox.Show("All Name Field Is Required!");
            }
        }

        private void btnNextStep3_Click(object sender, RoutedEventArgs e)
        {
            //dtStep4.Columns.Add("*");
            //foreach (ColumnDetail item in pnlColDetails.Children)
            //{
            //    //dtStep3.Columns.Add(item.txtBoxColName.Text);
            //    dtStep4.Columns.Add(item.txtName.Text);
            //    inputWeight.Add(Convert.ToDouble(item.txtWeight.Text));
            //    inputSign.Add(item.NumValue);
            //}
            //foreach (RowDetail item in pnlRowDetails.Children)
            //{
            //    DataRow newRow = dtStep4.NewRow();
            //    newRow[0] = item.txtName.Text;
            //    dtStep4.Rows.Add(newRow);
            //}


            ApiConnection.Connections connections = new ApiConnection.Connections();
            int ressss = connections.checkmoney(dtStep3.Columns.Count - 1, dtStep3.Rows.Count);
            if (ressss == 1)
            {
                Calculate_Topsis(inputSign, inputWeight, dtStep3);

                dgvStep4.Items.Refresh();
                //Disable Other Tabs
                if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                    return; // No more tabs to show!

                var currentTab = TabControlMain.SelectedItem as TabItem;
                currentTab.IsEnabled = false;
                var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex + 1] as TabItem;
                nextTab.IsEnabled = true;
                TabControlMain.SelectedItem = nextTab;
                //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
            }
            else if (ressss == -1)
            {
                MessageBox.Show("Low money");

            }
            else
            {
                MessageBox.Show("Cannot connect to the internet");
            }
        }

        private void btnNextStep4_Click(object sender, RoutedEventArgs e)
        {
            //--------------------------------------------------- Fill result text here
            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex + 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;

        }
        #endregion
        // ------------------------------------------------------ Previous Steps Buttons
        #region Previous Steps
        private void btnPreviousStep2_Click(object sender, RoutedEventArgs e)
        {
            Rows = 0;
            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex - 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
        }

        private void btnPreviousStep3_Click(object sender, RoutedEventArgs e)
        {
            dtStep3.Clear();
            dtStep3.Rows.Clear();
            dtStep3.Columns.Clear();

            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex - 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
        }

        private void btnPreviousStep4_Click(object sender, RoutedEventArgs e)
        {
            dtStep4.Clear();
            dtStep4.Rows.Clear();
            dtStep4.Columns.Clear();

            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex - 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
        }

        private void btnPreviousStep5_Click(object sender, RoutedEventArgs e)
        {
            results.Text = "";
            //Disable Other Tabs
            if (TabControlMain.Items.Count - 1 == TabControlMain.SelectedIndex)
                return; // No more tabs to show!

            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex - 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
            //tabControl.SelectedIndex = tabControl.SelectedIndex + 1;
        }
        #endregion




        public void Calculate_Topsis(List<int> sign1, List<double> weight1, DataTable dt1)
        {

            List<double> RadikalZigmaTavan2 = new List<double>();
            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                double ReslutOfColumn = 0;
                for (int j = 0; j < dt1.Rows.Count ; j++)
                {
                    ReslutOfColumn += Math.Pow(Convert.ToDouble(dt1.Rows[j][i].ToString()), 2);
                }
                RadikalZigmaTavan2.Add(Math.Pow(ReslutOfColumn, 0.5));
            }
            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                for (int j = 0; j < dt1.Rows.Count  ; j++)
                {
                    dt1.Rows[j][i] = ((Convert.ToDouble(dt1.Rows[j][i]) / RadikalZigmaTavan2[i-1]) * weight1[i-1]).ToString();
                }
            }
            List<List<double>> Meeyaar = new List<List<double>>();
            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                List<double> MeyarOfColumn = new List<double>();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    MeyarOfColumn.Add(Convert.ToDouble(dt1.Rows[j][i].ToString()));
                }
                Meeyaar.Add(MeyarOfColumn);
            }
            List<double> IdeAlMosbat = new List<double>();
            for (int i = 0; i < Meeyaar.Count; i++)
            {
                if (sign1[i] == 1)
                {
                    IdeAlMosbat.Add(Meeyaar[i].Max());
                }
                else
                {
                    IdeAlMosbat.Add(Meeyaar[i].Min());
                }
            }
            List<double> IdeAlManfi = new List<double>();
            for (int i = 0; i < Meeyaar.Count; i++)
            {
                if (sign1[i] == 1)
                {
                    IdeAlManfi.Add(Meeyaar[i].Min());
                }
                else
                {
                    IdeAlManfi.Add(Meeyaar[i].Max());
                }
            }
            //========================================================

            List<double> SiPlus = new List<double>();
            for (int j = 0; j < dt1.Rows.Count - 1; j++)
            {
                double ZigmaPluspow2 = 0;
                for (int i = 1; i < dt1.Columns.Count; i++)
                {
                    double Results1 = Convert.ToDouble(dt1.Rows[j][i].ToString()) - IdeAlMosbat[i-1];
                    ZigmaPluspow2 += Math.Pow(Results1, 2);
                }
                SiPlus.Add(Math.Pow(ZigmaPluspow2, 0.5));
            }

            List<double> Siminus = new List<double>();
            for (int j = 0; j < dt1.Rows.Count - 1; j++)
            {
                double ZigmaPluspow2 = 0;
                for (int i = 1; i < dt1.Columns.Count; i++)
                {
                    double Results1 = Convert.ToDouble(dt1.Rows[j][i].ToString()) - IdeAlManfi[i-1];
                    ZigmaPluspow2 += Math.Pow(Results1, 2);
                }
                Siminus.Add(Math.Pow(ZigmaPluspow2, 0.5));
            }
            List<double> ResultsOfSiPlusandMinus = new List<double>();
            for (int i = 0; i < Siminus.Count; i++)
            {
                ResultsOfSiPlusandMinus.Add((Siminus[i] / (Siminus[i] + SiPlus[i])));
            }
            ResultsOfSiPlusandMinus.Sort();
            string AAA = "";
            dgvStep4.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt1w });

            for (int i = 0; i < ResultsOfSiPlusandMinus.Count; i++)
            {
                AAA += "Topsis==> Num " + i.ToString() + " : " + ResultsOfSiPlusandMinus[i] + "\n";
            }
            results.Text = AAA;
        }






    }

}
