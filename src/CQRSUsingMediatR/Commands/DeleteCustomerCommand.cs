using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

namespace CQRSUsingMediatR.Commands
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public string customer_id { get; set; }
    }
}
