using System;

namespace chamaovar.Components.Code
{
	public static class InputValidation
	{
        public static ValidationResult ValidarLogin(string? email, string? senha)
        {
            ValidationResult resultado = new ValidationResult();

            if (email == "")
            {
                resultado.success = false;
                resultado.textResult = "Por favor, Digite um email";
            }
            else if (senha == "")
            {
                resultado.success = false;
                resultado.textResult = "Por favor, insira a senha";
            }
            else
            {
                resultado.success = true;
            }

            return resultado;
        }

		public static ValidationResult ValidarCriarConta(string? cpf,
            string? email, string? telefone, DateTime datanasc, string? name,
            string? password, string? confirmPassword, bool aceitouTermos)
		{
            ValidationResult resultado = new ValidationResult();

            if (!ValidarCPF(cpf))
            {
                resultado.success = false;
                resultado.textResult = "O CPF está inválido";
            }
            else if (!ValidarEmail(email))
            {
                resultado.success = false;
                resultado.textResult = "O Email está inválido!";
            }
            else if (!ValidarTelefone(telefone))
            {
                resultado.success = false;
                resultado.textResult = "O telefone está inválido";
            }
            else if (!ValidarDataNasc(datanasc))
            {
                resultado.success = false;
                resultado.textResult = "É necessário possuir 14 anos para usar o CHAMA O VAR";
            }
            else if (!ValidarNome(name))
            {
                resultado.success = false;
                resultado.textResult = "Nome inválido!";
            }
            else if (!ValidarSenha(password))
            {
                resultado.success = false;
                resultado.textResult = "A senha precisa de mais de 8 caracteres";
            }
            else if (!SenhasCoincidem(password, confirmPassword))
            {
                resultado.success = false;
                resultado.textResult = "Os campos de senha não coincidem";
            }
            else if (!aceitouTermos)
            {
                resultado.success = false;
                resultado.textResult = "Para o uso do CHAMA O VAR, aceitar o termos é necessário";
            }
            else
            {
                resultado.success = true;
                resultado.textResult = "Sucesso";
            }

            return resultado;
		}

        public static bool SenhasCoincidem(string senha1, string senha2)
        {
            return senha1 == senha2;
        }

        public static bool ValidarSenha(string senha)
        {
            return senha.Length > 8;
        }

        public static bool ValidarNome(string nome)
        {
            return nome.Length >= 8;
        }

        public static bool ValidarDataNasc(DateTime data)
        {
            int idade = DateTime.Now.Year - data.Year;

            return idade >= 14;
        }

        public static bool ValidarTelefone(string telefone)
        {
            return telefone.Length == 11;
        }

        public static bool ValidarCPF(string cpf)
        {
            return cpf.Length == 11;
        }

		public static bool ValidarEmail(string email)
		{
            // Tirar espaços nos cantos
            email = email.Trim();

            // Se termina em .
            if (email.EndsWith("."))
            {
                return false;
            }

            try
            {
                // Verifica se o endereço faz sentido como um email
                var enderecar = new System.Net.Mail.MailAddress(email);
                return enderecar.Address == email;
            }
            catch
            {
                // Se não for, voltar como false
                return false;
            }
        }
	}

	public class ValidationResult
	{
		// Propriedades
		public bool success;
		public string textResult;
	}
}

