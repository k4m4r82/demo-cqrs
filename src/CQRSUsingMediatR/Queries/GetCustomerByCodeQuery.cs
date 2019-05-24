using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;

namespace CQRSUsingMediatR.Queries
{
    public class GetCustomerByCodeQuery : IRequest<Customer>
    {
        public string CustomerId { get; set; }
    }
}
