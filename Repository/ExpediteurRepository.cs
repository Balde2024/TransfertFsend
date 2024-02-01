using System;
using AutoMapper;
using ForSendKH.Contracts;
using ForSendKH.Data.DataContext;
using ForSendKH.Models;
using Microsoft.EntityFrameworkCore;

namespace ForSendKH.Repository
{
    public class ExpediteurRepository : GenericRepository<Expediteur>, IExpediteurRepository
    {
        public readonly TransfertDbContext _context;

        public ExpediteurRepository(TransfertDbContext context, IMapper mapper) : base(context,mapper)
        {
            this._context = context;
        }

        public async Task<Expediteur> GetDetails(int? id)
        {
            return await _context.Expediteurs.Include(p => p.Transferts)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

