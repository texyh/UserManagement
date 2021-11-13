using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Shared.Abstractions
{
    public interface IReadOnlyRepository<T>
    {
        Task<T> FindBy(Guid id);
    }
}
