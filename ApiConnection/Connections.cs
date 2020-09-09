using Newtonsoft.Json;
using ProjectMainProp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiConnection
{
    public class Connections
    {
        public int checkmoney(int col, int row)
        {

            if (col >= 3 && row >= 3)
            {
                string json = File.ReadAllText("User.McdM");
                UserStruct StaticUserStruct = JsonConvert.DeserializeObject<UserStruct>(json);
                WebClient wc = new WebClient();
                string urls = $"{ProjectProp.MasterUrl}/CustomerSideUserApp/UserPay?_ui={StaticUserStruct.uid}";
                string results = wc.DownloadString(urls);
                Int64 money = Convert.ToInt64(results.ToString());
                if (money == -1)
                {
                    return 0;
                }
                else if (money == -2)
                {
                    return -1;

                }
                else if (money == -3)
                {
                    return 0;
                }
                else
                {
                    StaticUserStruct.MoneyRemains = money.ToString();
                    File.WriteAllText("User.McdM", JsonConvert.SerializeObject(StaticUserStruct));
                    return 1;
                }
            }
            else
            {
                return 1;
            }

        }

    }
}
