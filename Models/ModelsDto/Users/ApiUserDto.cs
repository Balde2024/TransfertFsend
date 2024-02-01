using System.ComponentModel.DataAnnotations;

namespace ForSendKH.Models.ModelsDto.Users
{
	public class ApiUserDto:LoginDto
	{
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }   
    }
}

