using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Model.DomainModel.Repository;
using System.Threading;
using CQRSUsingMediatR.Queries;

namespace CQRSUsingMediatR.QueryHandler
{
    public class GetCustomerByCodeQueryHandler : IRequestHandler<GetCustomerByCodeQuery, Customer>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByCodeQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(GetCustomerByCodeQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.GetById(request.CustomerId);
            return obj;
        }
    }
}
