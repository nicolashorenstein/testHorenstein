using Core.Commands;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ServiceRequestRepository : IServiceRepository
    {
        private readonly DBContext db;
        public ServiceRequestRepository(DBContext context)
        {
            db = context;
        }
        public async Task<SRequest> CreateServiceRequestAsync(SRequest model)
        {

            db.serviceRequests.Add(model);
            await db.SaveChangesAsync();

            return model;
        }

      

        public async Task<List<SRequest>> GetAllServiceRequestAsync()
        {
            return await db.serviceRequests.ToListAsync();
        }

        public async Task<SRequest> GetServiceRequestByIdAsync(Guid id)
        {
            return await db.serviceRequests.FirstOrDefaultAsync(request => request.id == id);
        }

     
        public async Task<SRequest> UpdateServiceRequestAsync(SRequest model)
        {
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteServiceRequestAsync(SRequest model)
        {
            db.Entry(model).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return true;
        }
    }
}
