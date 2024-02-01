using System;
using System.ComponentModel.DataAnnotations;

namespace ForSendKH.Models.ModelsDto.Users
{
	public class LoginDto
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Votre mot de passe doit etre compris entre {2} et {1} caracteres ")]
        public string Password { get; set; }
    }
}

