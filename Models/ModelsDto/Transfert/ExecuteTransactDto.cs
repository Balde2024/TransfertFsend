using System;
using System.ComponentModel.DataAnnotations;

namespace ForSendKH.Models.ModelsDto.Transfert
{
	public class ExecuteTransactDto
	{
        public int Id { get; set; }
        [Required]
        public string CodeTransfert { get; set; }
        public DateTime DateDeTransmission { get; set; }
        public string EtatDuTransfert { get; set; }
    }
}

