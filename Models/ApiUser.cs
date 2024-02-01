using System;
using Microsoft.AspNetCore.Identity;

namespace ForSendKH.Models
{
	public class ApiUser : IdentityUser
	{
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public virtual List<Transfert> Transferts { get; set; }
    }
}

