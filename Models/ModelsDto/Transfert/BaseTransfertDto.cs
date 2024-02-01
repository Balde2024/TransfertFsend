using System;
using System.ComponentModel.DataAnnotations;

namespace ForSendKH.Models.ModelsDto.Transfert
{
	public class BaseTransfertDto
    {
        [Required]
        public double Montant { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ExpediteurId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BeneficiereId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

