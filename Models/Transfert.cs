using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForSendKH.Models
{
	public class Transfert
	{
		public int Id { get; set; }
		public double Montant { get; set; }
		public double Commission { get; set; }
		public string CodeDeRetrait { get; set; }
		public string EtatDuTransfert { get; set; }
		public DateTime DateDuTransfert { get; set; }
        public DateTime? DateDeTransmission { get; set; }

        [ForeignKey(nameof(ExpediteurId))]
        public int ExpediteurId { get; set; }
		public Expediteur Expediteurs { get; set; }

        [ForeignKey(nameof(BeneficiereId))]
        public int BeneficiereId { get; set; }
        public Beneficiere Beneficiere { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }

    }
}

