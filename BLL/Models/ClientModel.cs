using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ClientModel
    {
        public string FIO { get; set; }

        public DateTime BirthDay { get; set; }

        public virtual Subscription Subscription { get; set; }
    }
}
