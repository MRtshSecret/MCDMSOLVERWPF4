﻿using ApiConnection;
using plgVikor.MainItems;
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

namespace plgVikor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class plgVikorWindow : UserControl
    {
        int Columns, Rows;
        DataTable dtStep3 = new DataTable();
        DataTable dtStep4 = new DataTable();
        List<decimal> inputSign = new List<decimal>();
        List<double> inputWeight = new List<double>();
        public string foldername = "";
        bool cangotostep3 = false;
        bool CanGoToStep2 = false;

        public plgVikorWindow()
        {
            InitializeComponent();
        }

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
            //dtStep4.Columns.Add("*");


            //int aa = dgvStep4.Columns.Count;
            //dgvStep4.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dtStep3 });
            //for (int i = 0; i < dgvStep4.Items.Count; i++)
            //{
            //    //dtgStep3.Items[i].Cells[1].ReadOnly = true;
            //    //dtgStep3.Rows[i].Cells[1].Style.BackColor = Color.Gray;
            //}
            //dgvStep4.Items.Refresh();
            Connections connections = new ApiConnection.Connections();
            int ressss = connections.checkmoney(dtStep3.Columns.Count - 1, dtStep3.Rows.Count);
            if (ressss == 1)
            {
                CalCulate_Vikor_Oghlidos(inputWeight, dtStep3);
                //============================================================================
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
            var nextTab = TabControlMain.Items[TabControlMain.SelectedIndex - 1] as TabItem;
            nextTab.IsEnabled = true;
            TabControlMain.SelectedItem = nextTab;
        }
        #endregion



        public void CalCulate_Vikor_Oghlidos(List<double> weight1, DataTable dt1, double _V = 0.5)
        {

            //List<double> ZigmaTavan2 = new List<double>();
            //for (int i = 0; i < dt1.Columns.Count; i++)
            //{
            //    double Result = 0;
            //    for (int j = 0; j < dt1.Rows.Count - 1; j++)
            //    {
            //        Result += Math.Pow(Convert.ToDouble(dt1.Rows[j][i].ToString()), 2);
            //    }
            //    ZigmaTavan2.Add(Result);
            //}

            //for (int i = 0; i < dt1.Columns.Count; i++)
            //{
            //    for (int j = 0; j < dt1.Rows.Count - 1; j++)
            //    {
            //        dt1.Rows[j][i] = (Convert.ToDouble(dt1.Rows[j][i].ToString()) / Math.Pow(ZigmaTavan2[i], 0.5)).ToString();
            //    }
            //}
            //=hashv
            List<List<string>> allv = new List<List<string>>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                List<string> a1 = new List<string>();
                for (int result = 0; result < dt1.Columns.Count; result++)
                {
                    a1.Add(dt1.Rows[i][result].ToString());
                }
                allv.Add(a1);
            }
            //==================

            List<double> Zigmarow = new List<double>();
            for (int j = 0; j < dt1.Columns.Count; j++)
            {
                double zigmacol = 0;
                for (int i = 0; i < dt1.Rows.Count - 1; i++)
                {
                    zigmacol += Convert.ToDouble(dt1.Rows[i][j].ToString());
                }
                Zigmarow.Add(zigmacol);
            }
            for (int j = 0; j < dt1.Columns.Count; j++)
            {
                for (int i = 0; i < dt1.Rows.Count - 1; i++)
                {
                    dt1.Rows[i][j] = (Convert.ToDouble(dt1.Rows[i][j].ToString()) / Zigmarow[j]);
                }
            }
            List<List<double>> Meeyar = new List<List<double>>();
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                List<double> MeeyarOfColumns = new List<double>();
                for (int j = 0; j < dt1.Rows.Count - 1; j++)
                {
                    MeeyarOfColumns.Add(Convert.ToDouble(dt1.Rows[j][i].ToString()));
                }
                Meeyar.Add(MeeyarOfColumns);
            }
            List<double> IdealPlus = new List<double>();
            for (int i = 0; i < Meeyar.Count; i++)
            {
                IdealPlus.Add(Meeyar[i].Max());
            }
            List<double> IdealMinus = new List<double>();
            for (int i = 0; i < Meeyar.Count; i++)
            {
                IdealMinus.Add(Meeyar[i].Min());
            }


            List<List<double>> Ri = new List<List<double>>();
            for (int i = 0; i < dt1.Rows.Count - 1; i++)
            {
                List<double> colri = new List<double>();
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    colri.Add(weight1[j] * ((IdealPlus[j] - Convert.ToDouble(dt1.Rows[i][j].ToString())) / (IdealPlus[j] - IdealMinus[j])));
                    dt1.Rows[i][j] = weight1[j] * ((IdealPlus[j] - Convert.ToDouble(dt1.Rows[i][j].ToString())) / (IdealPlus[j] - IdealMinus[j]));
                }
                Ri.Add(colri);
            }
            List<double> Si = new List<double>();
            for (int i = 0; i < Ri.Count; i++)
            {
                double sumrow = 0;
                for (int j = 0; j < Ri[i].Count; j++)
                {
                    sumrow += Ri[i][j];
                }
                Si.Add(sumrow);
            }
            List<double> RIMAX = new List<double>();
            for (int i = 0; i < Ri.Count; i++)
            {
                RIMAX.Add(Ri[i].Max());
            }
            double RStar = RIMAX.Max();
            double SStar = Si.Max();
            double RMinus = RIMAX.Min();
            double SMinus = Si.Min();

            List<double> Qs = new List<double>();
            for (int i = 0; i < Si.Count; i++)
            {
                Qs.Add(_V * ((Si[i] + SMinus) / (SStar - SMinus)) + (1 - _V) * ((RIMAX[i] - RMinus) / (RStar - RMinus)));
            }
            string AAA = "";
            for (int i = 0; i < Qs.Count; i++)
            {
                AAA += "Vikor ==> Num " + (i + 1).ToString() + " : " + Qs[i].ToString() + "\n";
            }
            results.Text = AAA;

        }

    }
}
