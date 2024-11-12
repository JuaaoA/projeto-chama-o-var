using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace chamaovar.Components.Code
{
	public class Ocorrencias
	{
		public int id { get; set; }
		public string acontecimento { get; set; }
        public DateTime data_ocorrencia { get; set; }
        public int penalidade { get; set; }
        public int torcedor { get; set; }
        public int colaborador { get; set; }
    }

	public static class OcorrenciasJSON
	{
		public static async Task<List<Ocorrencias>?> JsonToOcorrencias()
		{
			// Criar cliente http
			HttpClient http = new HttpClient();

			// Pegar todas as ocorrências
			var ocorrencias = await http.GetAsync("https://localhost:7094/chamaovar-api/ocorrencia");

			Console.WriteLine(await ocorrencias.Content.ReadAsStringAsync());

			// Pegar o status code retornado
			int codigoResult = (int)ocorrencias.StatusCode;

			// Escolher o que fazer de acordo com o código
			if (codigoResult != 200)
			{
				return null;
			}

			// Se deu tudo certo
			List<Ocorrencias>? ocorrencias1 = JsonSerializer.Deserialize<List<Ocorrencias>>(
					await ocorrencias.Content.ReadAsStringAsync()
				) ;

			// Retonar
			return ocorrencias1;
		}
	}
}

