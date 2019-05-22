using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSUsingMediatR.Model.DomainModel.Entity
{
    public class Customer
    {
        public string customer_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
}
