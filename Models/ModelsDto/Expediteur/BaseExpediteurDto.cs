using System.ComponentModel.DataAnnotations;

namespace ForSendKH.Models.ModelsDto.Expediteur
{
	public class BaseExpediteurDto
	{
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Pays { get; set; }

        [Required]
        public string Ville { get; set; }
    }
}

