using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcdm.Controllers
{
    class UserStruct
    {
        public string uid { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string MoneyRemains { get; set; }
        public DateTime UserRegDate { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastName { get; set; }
        public DateTime requestDatetime { get; set; }
    }
}
