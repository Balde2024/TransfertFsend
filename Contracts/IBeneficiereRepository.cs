using System;
using ForSendKH.Models;

namespace ForSendKH.Contracts
{
	public interface IBeneficiereRepository: IGenericRepository<Beneficiere>
	{
        public Task<Beneficiere> GetDetails(int? id);
    }
}

