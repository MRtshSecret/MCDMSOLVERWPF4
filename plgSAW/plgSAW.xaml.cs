using plgSAW.MainItems;
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

namespace plgSAW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class plgSAWWindow : UserControl
    {
        int Columns, Rows;
        DataTable dtStep3 = new DataTable();
        DataTable dtStep4 = new DataTable();
        List<int> inputSign = new List<int>();
        List<double> inputWeight = new List<double>();
        public string foldername = "";

        public plgSAWWindow()
        {
            InitializeComponent();
        }
        bool cangotostep3 = false;
        private void CBBRowCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cangotostep3 = true;
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

        bool CanGoToStep2 = false;
        private void CBBColCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanGoToStep2 = true;
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
            if (CanGoToStep2)
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
            else
            {
                MessageBox.Show("Please InsertRows!");
            }
        }


        private void btnNextStep2_Click(object sender, RoutedEventArgs e)
        {
            if (cangotostep3)
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
            else
            {
                MessageBox.Show("Please insert Column");
            }


        }

        private void btnNextStep3_Click(object sender, RoutedEventArgs e)
        {
            ApiConnection.Connections connections = new ApiConnection.Connections();
            int ressss = connections.checkmoney(dtStep3.Columns.Count - 1, dtStep3.Rows.Count);
            if (ressss == 1)
            {
                //=====================Todelete
                inputWeight.Add(0.2);
                inputWeight.Add(0.15);
                inputWeight.Add(0.15);
                inputWeight.Add(0.3);
                inputWeight.Add(0.2);
                inputSign.Add(-1);
                inputSign.Add(1);
                inputSign.Add(-1);
                inputSign.Add(1);
                inputSign.Add(1);

                //=====================Todelete
                Calculate_SAW(inputSign, inputWeight, dtStep3);
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
            var currentTab = TabControlMain.SelectedItem as TabItem;
            currentTab.IsEnabled = false;
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex -1 ] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
        }
        #endregion

        public void Calculate_SAW(List<int> sign1, List<double> weight1, DataTable dt1)
        {
            List<double> Taghsimehar_sotoon = new List<double>();
            List<List<double>> harsotoon = new List<List<double>>();
            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                List<double> Sotoons = new List<double>();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Sotoons.Add(Convert.ToDouble(dt1.Rows[j][i]));
                }
                harsotoon.Add(Sotoons);
            }
            List<double> Meyar = new List<double>();
            for (int i = 0; i < harsotoon.Count; i++)
            {
                if (sign1[i] == 1)
                {
                    Meyar.Add(harsotoon[i].Max());
                }
                else
                {
                    Meyar.Add(harsotoon[i].Min());
                }
            }

            for (int i = 1; i < dt1.Columns.Count; i++)
            {
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (sign1[i - 1] == 1)
                    {
                        double Result = ((Convert.ToDouble(dt1.Rows[j][i].ToString())) / Meyar[i - 1]) * weight1[i - 1];
                        dt1.Rows[j][i] = Result.ToString();
                    }
                    else
                    {
                        double Result = (Meyar[i - 1] / (Convert.ToDouble(dt1.Rows[j][i].ToString()))) * weight1[i - 1];
                        dt1.Rows[j][i] = Result.ToString();
                    }
                }
            }
            List<double> Sorts = new List<double>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                double Sum = 0;
                for (int j = 1; j < dt1.Columns.Count; j++)
                {
                    Sum += Convert.ToDouble(dt1.Rows[i][j].ToString());
                }
                Sorts.Add(Sum);
            }
            Sorts.Sort();
            string AAA = "";
            dgvStep4.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt1 });
            dgvStep4.Items.Refresh();
            for (int i = 0; i < Sorts.Count; i++)
            {
                AAA += "SAW ==> Num " + i.ToString() + " ==> " + Sorts[i] + "\n";
            }
            results.Text = AAA;
        }



    }
}
