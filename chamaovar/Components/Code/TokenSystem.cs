using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace chamaovar.Components.Code
{
	public static class TokenSystem
	{
		public static void InserirNovoToken(string token)
		{
			//
			Token newToken = new Token();

			newToken.nome = token;

			string fileName = "token.json";
			string jsonString = JsonSerializer.Serialize(newToken);
			File.WriteAllText(fileName, jsonString);
		}

		public static void Deslogar()
		{
			Token voidToken = new Token();
			voidToken.nome = "";

            string fileName = "token.json";
            string jsonString = JsonSerializer.Serialize(voidToken);
            File.WriteAllText(fileName, jsonString);
        }

		public static async Task<Torcedor?> GetTorcedorByToken()
		{
			// Iniciar http
			HttpClient http = new HttpClient();

			// Pegar o token
			Token? tk = GetToken();

			// Se o token for nulo
			if (tk == null)
			{
				return null;
			}

			// Realizar um Get do torcedor
			var resultado = await http.GetAsync($"https://localhost:7094/chamaovar-api/torcedor/token?nome_token={tk.nome}");

			// Pegar o status code do get
			int statusCode = (int)resultado.StatusCode;

			// Se o Resultado deu 200, ou seja, deu certo
			if (statusCode == 200)
			{
				// Pegar o torcedor no json indicado
				Torcedor? tcdr = JsonSerializer.Deserialize<Torcedor>(await resultado.Content.ReadAsStringAsync());

				// Retornar o torcedor
				return tcdr;
			}

			// Caso o resultado tenha dado erro, retornar nulo
			return null;
		}

		public static async Task<Torcedor?> GetTorcedorById(int id)
		{
            // Iniciar http
            HttpClient http = new HttpClient();

            // Realizar um Get do torcedor
            var resultado = await http.GetAsync($"https://localhost:7094/chamaovar-api/torcedor/get?torcedorId={id}");

			// Pegar o status code do get
			int statusCode = (int)resultado.StatusCode;

			// Se o Resultado deu 200, ou seja, deu certo
			if (statusCode == 200)
			{
				// Pegar o torcedor no json indicado
				Torcedor? tcdr = JsonSerializer.Deserialize<Torcedor>(await resultado.Content.ReadAsStringAsync());

				// Retornar o torcedor
				return tcdr;
			}

            // Caso o resultado tenha dado erro, retornar nulo
            return null;
        }

		public static async Task<List<Torcedor>?> GetAllTorcedor()
		{
            // Iniciar http
            HttpClient http = new HttpClient();

            // Realizar um Get do torcedor
            var resultado = await http.GetAsync($"https://localhost:7094/chamaovar-api/torcedor");

            // Pegar o status code do get
            int statusCode = (int)resultado.StatusCode;

            // Se o Resultado deu 200, ou seja, deu certo
            if (statusCode == 200)
            {
                // Pegar o torcedor no json indicado
                List<Torcedor>? tcdr = JsonSerializer.Deserialize<List<Torcedor>>(await resultado.Content.ReadAsStringAsync());

                // Retornar o torcedor
                return tcdr;
            }

            // Caso o resultado tenha dado erro, retornar nulo
            return null;
        }

        public static Token? GetToken()
		{
			// Ler o arquivo de json e armazenar em texto numa variável
			string jsonToken = File.ReadAllText("token.json");

			// Deserializar o json e transformar em objeto Token
			Token? tk = JsonSerializer.Deserialize<Token>(jsonToken);

			return tk;
        }

	}

	public class Token
	{
		public string nome { get; set; }
	}
}

