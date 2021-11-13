using Marten;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Shared.Abstractions;

namespace UserManagement.Infrastructure.Repositories
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T>
    {
        private readonly IDocumentStore _store;
        public ReadOnlyRepository(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<T> FindBy(Guid id)
        {
            using(var session = _store.OpenSession())
            {
                return await session.LoadAsync<T>(id);
            }
        }
    }
}
