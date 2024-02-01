using System;
using AutoMapper;
using ForSendKH.Contracts;
using ForSendKH.Data.DataContext;
using ForSendKH.Models;
using Microsoft.EntityFrameworkCore;

namespace ForSendKH.Repository
{
	public class BeneficiereRepository:GenericRepository<Beneficiere> , IBeneficiereRepository
	{
		public readonly TransfertDbContext _context;

        public BeneficiereRepository(TransfertDbContext context, IMapper mapper) : base(context,mapper)
        {
            this._context = context;
        }

        public async Task<Beneficiere> GetDetails(int? id)
        {
            return await _context.Beneficieres.Include(p => p.Transferts)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

