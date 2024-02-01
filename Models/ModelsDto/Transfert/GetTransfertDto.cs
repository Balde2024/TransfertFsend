using System;
namespace ForSendKH.Models.ModelsDto.Transfert
{
	public class GetTransfertDto:BaseTransfertDto
	{
		public int Id { get; set; }
        public DateTime DateDuTransfert { get; set; }
        public double Commission { get; set; }
        public string CodeDeRetrait { get; set; }
        public string EtatDuTransfert { get; set; }
        public DateTime? DateDeTransmission { get; set; }
    }
}

