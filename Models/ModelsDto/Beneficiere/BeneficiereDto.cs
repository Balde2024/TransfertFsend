using System;
using ForSendKH.Models.ModelsDto.Transfert;

namespace ForSendKH.Models.ModelsDto.Beneficiere
{
	public class BeneficiereDto: BaseBeneficiereDto
	{
        public int Id { get; set; }

        public virtual IList<TransfertDto> Transferts { get; set; }
    }
}

