using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Model.DomainModel.Repository;
using CQRSUsingMediatR.Queries;

namespace CQRSUsingMediatR.QueryHandler
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepository _repository;

        public GetAllCustomersQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAll();
            return list;
        }
    }
}
