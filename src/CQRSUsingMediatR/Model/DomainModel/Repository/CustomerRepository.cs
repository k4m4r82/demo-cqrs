using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper;
using CQRSUsingMediatR.Model.Context;
using CQRSUsingMediatR.Model.DomainModel.Entity;
using Microsoft.Extensions.Logging;

namespace CQRSUsingMediatR.Model.DomainModel.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetById(string id);
        Task<List<Customer>> GetByName(string name);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContext _context;
        private readonly ILogger _logger;

        public CustomerRepository(IDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CustomerRepository>();
        }

        public Task<Customer> GetById(string id)
        {
            Customer obj = null;

            try
            {
                var sql = @"select customer_id, name, address 
                            from customers
                            where customer_id = @customer_id";

                obj = _context.Conn.QuerySingleOrDefaultAsync<Customer>(sql, new { customer_id = id }).Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex.Message);
            }

            return Task.FromResult(obj);
        }

        public Task<List<Customer>> GetAll()
        {
            IEnumerable<Customer> list = new List<Customer>();

            try
            {
                var sql = @"select customer_id, name, address 
                            from customers
                            order by name";

                list = _context.Conn.QueryAsync<Customer>(sql).Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex.Message);
            }

            return Task.FromResult(list.ToList());
        }

        public Task<List<Customer>> GetByName(string name)
        {
            IEnumerable<Customer> list = new List<Customer>();

            try
            {
                var sql = @"select customer_id, name, address 
                            from customers
                            where name like @name
                            order by name";

                name = "%" + name.ToLower() + "%";
                list = _context.Conn.QueryAsync<Customer>(sql, new { name }).Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex.Message);
            }

            return Task.FromResult(list.ToList());
        }

        public Task<int> Save(Customer obj)
        {
            var result = 0;

            try
            {
                var sql = @"insert into customers (customer_id, name, address)
                            values (@customer_id, @name, @address)";

                result = _context.Conn.ExecuteAsync(sql, obj).Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex.Message);
            }

            return Task.FromResult(result);
        }

        public Task<int> Update(Customer obj)
        {
            var result = 0;

            try
            {
                var sql = @"update customers set name = @name, address = @address
                            where customer_id = @customer_id";

                result = _context.Conn.ExecuteAsync(sql, obj).Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex.Message);
            }

            return Task.FromResult(result);
        }

        public Task<int> Delete(Customer obj)
        {
            var result = 0;

            try
            {
                var sql = @"delete from customers
                            where customer_id = @customer_id";

                result = _context.Conn.ExecuteAsync(sql, new { customer_id = obj.customer_id }).Result;

            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex.Message);
            }

            return Task.FromResult(result);
        }        
    }
}
