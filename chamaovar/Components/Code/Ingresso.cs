using System;
using System.Text.Json;

namespace chamaovar.Components.Code
{
	public class Ingresso
	{
		public Ingresso()
		{
		}
        public int id { get; set; }
        public int torcedor { get; set; }
        public int evento { get; set; }
    }

	public static class IngressoJSON
	{
		public static async Task<List<Ingresso>?> JsonToListIngressoByEvento(int eventoId)
		{
            // Criar cliente http
            HttpClient http = new HttpClient();

            // Get do evento
            var ingressos = await http.GetAsync($"https://localhost:7094/chamaovar-api/ingresso/ByEvento?eventoId={eventoId}");

            // Pegar status code
            int status = (int)ingressos.StatusCode;

            // Se deu errado
            if (status != 200)
            {
                return null;
            }

            // Se não
            List<Ingresso>? ingressos1 = JsonSerializer.Deserialize<List<Ingresso>>(
                    await ingressos.Content.ReadAsStringAsync()
                );

            // Retornar ingressos
            return ingressos1;
        }
	}
}

