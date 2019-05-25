using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Repository;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Commands;
using System.Threading;

namespace CQRSUsingMediatR.CommandHandlers
{
    public class CustomerCommandHandler : IRequestHandler<CreateUpdateCustomerCommand, bool>,
                                          IRequestHandler<DeleteCustomerCommandX, bool>
    {
        private readonly ICustomerRepository _repository;

        public CustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateUpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var isNewEntity = false;

            var entity = await _repository.GetById(request.customer_id);

            if (entity == null)
            {
                isNewEntity = true;

                entity = new Customer();
                entity.customer_id = request.customer_id;
            }

            entity.name = request.name;
            entity.address = request.address;

            var result = await (isNewEntity ? _repository.Save(entity) : _repository.Update(entity));
            return result > 0;
        }

        public async Task<bool> Handle(DeleteCustomerCommandX request, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(new Customer { customer_id = request.customer_id });
            return result > 0;
        }
    }
}
