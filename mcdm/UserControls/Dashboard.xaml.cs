using mcdm.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            string json = File.ReadAllText("User.McdM");
            UserStruct StaticUserStruct = JsonConvert.DeserializeObject<UserStruct>(json);

         

            InitializeComponent();
            txt_UserNotes.Text = $"Name: {StaticUserStruct.UserFirstname}\nLastName: {StaticUserStruct.UserLastName}\nMoneyRemaining: {StaticUserStruct.MoneyRemains} Tooman\nRegDate: {StaticUserStruct.UserRegDate}";

        }
    }
}
