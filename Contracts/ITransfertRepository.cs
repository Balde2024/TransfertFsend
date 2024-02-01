using System;
using ForSendKH.Models;
using ForSendKH.Models.ModelsDto.Transfert;

namespace ForSendKH.Contracts
{
	public interface ITransfertRepository: IGenericRepository<Transfert>
	{
        public void InitializeData(Transfert transfert);
        public void Init(Transfert transfert);
        public Task<Transfert> GetFerifyCode(string codeTransfert);
        public Task<Transfert> GetVerifyEtat(string codeTransfert);
    }
}

