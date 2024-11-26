using System;
using System.Text.Json;

namespace chamaovar.Components.Code
{
	public class Evento
	{
		public Evento()
		{
		}

        public int id { get; set; }
        public string nome { get; set; }
        public DateTime data_evento { get; set; }
        public string detalhes { get; set; }
        public int minimo_pontuacao { get; set; }
        public int criador { get; set; }
    }

    public static class EventosJSON
    {
        public static async Task<List<Evento>?> JsonToEventos()
        {
            // Criar cliente http
            HttpClient http = new HttpClient();

            // Pegar todas as ocorrências
            HttpResponseMessage eventos;

            eventos = await http.GetAsync($"https://localhost:7094/chamaovar-api/evento");

            Console.WriteLine(await eventos.Content.ReadAsStringAsync());

            // Pegar o status code retornado
            int codigoResult = (int)eventos.StatusCode;

            // Escolher o que fazer de acordo com o código
            if (codigoResult != 200)
            {
                return null;
            }

            // Se deu tudo certo
            List<Evento>? eventos1 = JsonSerializer.Deserialize<List<Evento>>(
                    await eventos.Content.ReadAsStringAsync()
                );

            // Retonar
            return eventos1;
        }

        public static async Task<Evento?> JsonToEventoByID(int id)
        {
            // Criar cliente http
            HttpClient http = new HttpClient();

            // Get do evento
            var evento = await http.GetAsync($"https://localhost:7094/chamaovar-api/evento/ByID?id={id}");

            // Pegar status code
            int status = (int)evento.StatusCode;

            // Se deu errado
            if (status != 200)
            {
                return null;
            }

            // Se deu certo
            Evento? evento1 = JsonSerializer.Deserialize<Evento>(
                    await evento.Content.ReadAsStringAsync()
                );

            // Retornar
            return evento1;
        }

        public static async Task<List<Evento>?> JsonToEventosByCriador(string token, bool inverterSelect)
        {
            // Criar cliente http
            HttpClient http = new HttpClient();

            // Pegar todas as ocorrências
            HttpResponseMessage eventos;

            if (!inverterSelect)
            {
                eventos = await http.GetAsync($"https://localhost:7094/chamaovar-api/evento/ByCriador?criadorToken={token}");
            }
            else
            {
                eventos = await http.GetAsync($"https://localhost:7094/chamaovar-api/evento/ByCriador/inverter?criadorToken={token}");
            }

            Console.WriteLine(await eventos.Content.ReadAsStringAsync());

            // Pegar o status code retornado
            int codigoResult = (int)eventos.StatusCode;

            // Escolher o que fazer de acordo com o código
            if (codigoResult != 200)
            {
                return null;
            }

            // Se deu tudo certo
            List<Evento>? eventos1 = JsonSerializer.Deserialize<List<Evento>>(
                    await eventos.Content.ReadAsStringAsync()
                );

            // Retonar
            return eventos1;
        }
    }
}

