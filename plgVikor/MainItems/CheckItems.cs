using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plgVikor.MainItems
{
    class CheckItemsCols
    {
        public CheckItemsCols(List<ColumnDetail> allColumnDetails)
        {
            AllColumnDetails = allColumnDetails;
            AllColumnDetailsCount = AllColumnDetails.Count;
        }
        private int AllColumnDetailsCount = 0;
        private List<ColumnDetail> AllColumnDetails { get; set; }
        public bool WeightSum()
        {
            //double inputValue = Math.Round(inputValue, 2);
            double Sum = 0.00;
            for (int i = 0; i < AllColumnDetailsCount; i++)
            {
                Sum += Math.Round(double.Parse(AllColumnDetails[i].txtWeight.Text), 2);
            }
            Sum = Math.Round(Sum, 2);
            if (Sum == 1.00)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ISAllNameFilled()
        {
            bool results = true;
            for (int i = 0; i < AllColumnDetailsCount; i++)
            {
                if (string.IsNullOrEmpty(AllColumnDetails[i].txtName.Text))
                {
                    results = false;
                    break;
                }
            }
            return results;
        }
    }

    class CheckItemsRows
    {
        public CheckItemsRows(List<RowDetail> allRowDetails)
        {
            AllRowDetails = allRowDetails;
            AllRowDetailsCount = AllRowDetails.Count;
        }
        private int AllRowDetailsCount = 0;
        private List<RowDetail> AllRowDetails { get; set; }

        public bool ISAllNameFilled()
        {
            bool results = true;
            for (int i = 0; i < AllRowDetailsCount; i++)
            {
                if (string.IsNullOrEmpty(AllRowDetails[i].txtName.Text))
                {
                    results = false;
                    break;
                }
            }
            return results;
        }
    }
}
