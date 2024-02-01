using System;
using ForSendKH.Models;

namespace ForSendKH.Contracts
{
	public interface IExpediteurRepository:IGenericRepository<Expediteur>
	{
		public Task<Expediteur> GetDetails(int? id);
	}
}

