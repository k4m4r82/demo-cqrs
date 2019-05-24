using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

namespace CQRSUsingMediatR.Commands
{
    public class CreateUpdateCustomerCommand : IRequest<bool>
    {
        public string customer_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class DeleteCustomerCommand : IRequest<bool>
    {
        public string customer_id { get; set; }
    }
}
