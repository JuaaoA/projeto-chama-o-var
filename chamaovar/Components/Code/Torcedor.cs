using System;
namespace chamaovar.Components.Code
{
	public class Torcedor
	{
		public Torcedor()
		{
		}


        public string nome_completo { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public int score { get; set; }
        public DateTime nascimento { get; set; }
        public string senha { get; set; }
        public bool tecnico { get; set; }
    }
}

