using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Repository;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Commands;

namespace CQRSUsingMediatR.CommandHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(new Customer { customer_id = request.customer_id });
            return result > 0;
        }
    }
}
