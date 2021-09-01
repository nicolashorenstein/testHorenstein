using Core.Commands;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<SRequest>> GetAllServiceRequestAsync();
        Task<SRequest> GetServiceRequestByIdAsync(Guid id);
        Task<SRequest> CreateServiceRequestAsync(SRequest srequest);
        Task<SRequest> UpdateServiceRequestAsync(SRequest srequest);
        Task<bool> DeleteServiceRequestAsync(SRequest srequest);

    }
}
