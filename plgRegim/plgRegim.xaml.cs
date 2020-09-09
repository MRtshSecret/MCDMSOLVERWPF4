using plgRegim.MainItems;
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

namespace plgRegim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class plgRegimWindow : UserControl
    {
        int Columns, Rows;
        DataTable dtStep3 = new DataTable();
        DataTable dtStep4 = new DataTable();
        List<bool> inputSign = new List<bool>();
        List<double> inputWeight = new List<double>();
        public string foldername = "";

        public plgRegimWindow()
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

        public void btnNextStep2_Click(object sender, RoutedEventArgs e)
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
                    if (item.NumValue.ToString() == "1")
                    {
                        inputSign.Add(true);
                    }
                    else
                    {
                        inputSign.Add(false);
                    }

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
            //int aa = dgvStep4.Columns.Count;
            //dgvStep4.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dtStep3 });
            //for (int i = 0; i < dgvStep4.Items.Count; i++)
            //{
            //    //dtgStep3.Items[i].Cells[1].ReadOnly = true;
            //    //dtgStep3.Rows[i].Cells[1].Style.BackColor = Color.Gray;
            //}
            ApiConnection.Connections connections = new ApiConnection.Connections();
            int ressss = connections.checkmoney(dtStep3.Columns.Count - 1, dtStep3.Rows.Count);
            if (ressss == 1)
            {

                CalculateAlgorythm(dtStep3, inputWeight, inputSign);
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
            else if(ressss == -1)
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
            results.Text = "results will be shown here\nfor now we got to show to 10 for example...";

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







        public void CalculateAlgorythm(DataTable dtDaryafti, List<double> Weights, List<bool> Sign)
        {
            List<DataTable> dtofmoreds = new List<DataTable>();
            for (int j = 1; j < dtDaryafti.Columns.Count; j++)
            {
                DataTable dt = new DataTable();
                for (int i = 0; i < dtDaryafti.Rows.Count; i++)
                {
                    dt.Columns.Add($"A{i}", typeof(double));
                    dt.Columns[$"A{i}"].Caption = $"A{i}";
                }

                for (int i = 0; i < dtDaryafti.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int jj = 0; jj < dtDaryafti.Rows.Count; jj++)
                    {
                        dr[jj] = 0;
                    }
                    dt.Rows.Add(dr);
                }

                for (int i = 0; i < dtDaryafti.Rows.Count; i++)
                {
                    for (int k = 0; k < dtDaryafti.Rows.Count; k++)
                    {
                        double Res = 0;
                        Res = Convert.ToDouble(dtDaryafti.Rows[i][j].ToString()) - Convert.ToDouble(dtDaryafti.Rows[k][j].ToString());
                        if (Sign[j - 1])// + is True -- - Is False
                        {
                            if (Res > 0)
                            {
                                dt.Rows[i][k] = 1;
                            }
                            else if (Res == 0)
                            {
                                dt.Rows[i][k] = 0;
                            }
                            else
                            {
                                dt.Rows[i][k] = -1;
                            }
                        }
                        else
                        {
                            if (Res > 0)
                            {
                                dt.Rows[i][k] = -1;
                            }
                            else if (Res == 0)
                            {
                                dt.Rows[i][k] = 0;
                            }
                            else
                            {
                                dt.Rows[i][k] = 1;
                            }
                        }
                    }
                }
                dtofmoreds.Add(dt);
            }
            int flagOfTables = dtDaryafti.Rows.Count;
            DataTable dtWeights = CREATEDatatable(flagOfTables, flagOfTables);
            for (int i = 0; i < flagOfTables; i++)
            {
                for (int j = 0; j < flagOfTables; j++)
                {
                    double res = 0;
                    for (int u = 0; u < dtofmoreds.Count; u++)
                    {
                        res += Convert.ToInt32((dtofmoreds[u]).Rows[i][j].ToString()) * Weights[u];
                    }
                    dtWeights.Rows[i][j] = res;
                }
            }
            //dataGridView2.DataSource = dtWeights;
            //=================================================Doroste
            DataTable KIREKHAR = CREATEDatatable(flagOfTables, flagOfTables);
            string kir = "";
            for (int i = 0; i < flagOfTables; i++)
            {
                for (int j = 0; j < flagOfTables; j++)
                {
                    if (i == j) { continue; }
                    int counts = 0;
                    for (int k = 0; k < flagOfTables; k++)
                    {
                        for (int kj = 0; kj < flagOfTables; kj++)
                        {
                            if (k == kj) { continue; }
                            if (i == k && j == kj) { continue; }
                            double Kir1 = Convert.ToDouble(dtWeights.Rows[i][j].ToString());
                            double kir2 = Convert.ToDouble(dtWeights.Rows[k][kj].ToString());
                            if (Convert.ToDouble(dtWeights.Rows[i][j].ToString()) >= Convert.ToDouble(dtWeights.Rows[k][kj].ToString()))
                            {
                                counts++;
                            }
                        }
                    }
                    double Taghsim = (flagOfTables * (flagOfTables - 1));
                    KIREKHAR.Rows[i][j] = ((counts) / Taghsim);
                    kir += $"V[{i + 1}][{j + 1}] : {counts} / {Taghsim} \n";
                }
            }
            string resultsss = "";
            resultsss += "First Result : " + kir;
            dgvStep4.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = KIREKHAR });
            List<StructFlagger> sf = new List<StructFlagger>();
            for (int i = 0; i < flagOfTables; i++)
            {
                StructFlagger flaggerZigma = new StructFlagger();
                flaggerZigma.IFlag = i;
                flaggerZigma.Value = 0;
                for (int j = 0; j < flagOfTables; j++)
                {
                    flaggerZigma.Value += Convert.ToDouble(KIREKHAR.Rows[i][j].ToString());
                }
                flaggerZigma.Value = flaggerZigma.Value / (flagOfTables - 1);
                sf.Add(flaggerZigma);
            }
            sf = sf.OrderBy(x => x.Value).ToList();
            string AA = "";
            for (int i = sf.Count - 1; i >= 0; i--)
            {
                AA += $"S{sf[i].IFlag + 1} : {sf[i].Value} || ";
            }

            resultsss += "\n----------------------------\nAfterSerialized Result : " + AA;
            results.Text = resultsss;
        }

        private DataTable CREATEDatatable(int rows, int cols)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < cols; i++)
            {
                dt.Columns.Add($"A{i}", typeof(string));
                dt.Columns[$"A{i}"].Caption = $"A{i}";
            }

            for (int i = 0; i < rows; i++)
            {
                DataRow dr = dt.NewRow();
                for (int jj = 0; jj < cols; jj++)
                {
                    dr[dt.Columns[dt.Columns[jj].Caption]] = "0";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
    public class StructFlagger
    {
        public int IFlag { get; set; }
        public double Value { set; get; }
    }
}
