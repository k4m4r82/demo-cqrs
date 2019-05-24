using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using CQRSUsingMediatR.Commands;
using CQRSUsingMediatR.Queries;

namespace CQRSUsingMediatR.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("get_by_id")]
        public async Task<IActionResult> GetById(string id)
        {
            var list = await _mediator.Send(new GetCustomerByCodeQuery { CustomerId = id });
            return Ok(list);
        }

        [HttpGet, Route("get_all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(list);
        }

        [HttpPost, Route("save")]
        public async Task<IActionResult> Save([FromBody] CreateUpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
