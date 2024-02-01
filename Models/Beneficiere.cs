using System;
namespace ForSendKH.Models
{
	public class Beneficiere
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string Telephone { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }

		public virtual IList<Transfert> Transferts { get; set; }
	}
}

