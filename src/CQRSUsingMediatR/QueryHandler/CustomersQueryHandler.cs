using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Model.DomainModel.Repository;
using CQRSUsingMediatR.Queries;

namespace CQRSUsingMediatR.QueryHandler
{
    public class CustomersQueryHandler : IRequestHandler<GetCustomerByCodeQuery, Customer>,
                                         IRequestHandler<GetAllCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepository _repository;

        public CustomersQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(GetCustomerByCodeQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.GetById(request.CustomerId);
            return obj;
        }

        public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAll();
            return list;
        }
    }
}
