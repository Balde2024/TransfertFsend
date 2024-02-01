using System;
using ForSendKH.Models.ModelsDto.Transfert;

namespace ForSendKH.Models.ModelsDto.Expediteur
{
	public class ExpediteurDto:BaseExpediteurDto
	{
        public int Id { get; set; }
      
        public virtual IList<TransfertDto> Transferts { get; set; }
    }
}

