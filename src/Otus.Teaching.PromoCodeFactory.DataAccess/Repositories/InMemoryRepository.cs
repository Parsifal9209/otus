using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>
        : IRepository<T>
        where T: BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }
        
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task CreateAsync(T entity)
        {
            Data = Data.Append(entity);

            return Task.CompletedTask;
        }

        public Task DeleteByIdAsync(Guid id)
        {
            Data = Data.Where(x => x.Id != id);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            Data = Data.Where(x => x.Id != entity.Id).Append(entity);

            return Task.CompletedTask;
        }
    }
}