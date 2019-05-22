using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Model.DomainModel.Repository;

namespace CQRSUsingMediatR.Queries
{
    public class CustomerQueryHandler : IRequestHandler<CustomerAllQuery, List<Customer>>
    {
        private readonly ICustomerRepository _repository;

        public CustomerQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> Handle(CustomerAllQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAll();
            return list;
        }
    }
}
