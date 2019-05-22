using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Model.DomainModel.Repository;
using System.Threading;

namespace CQRSUsingMediatR.Queries
{
    public class CustomerByCodeQueryHandler : IRequestHandler<CustomerByCodeQuery, Customer>
    {
        private readonly ICustomerRepository _repository;

        public CustomerByCodeQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(CustomerByCodeQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.GetById(request.CustomerId);
            return obj;
        }
    }
}
