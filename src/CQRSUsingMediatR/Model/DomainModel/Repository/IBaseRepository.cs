using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSUsingMediatR.Model.DomainModel.Repository
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<int> Save(T obj);
        Task<int> Update(T obj);
        Task<int> Delete(T obj);
        Task<List<T>> GetAll();
    }
}
