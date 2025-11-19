using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace CriptoBiyuya.Services
{
    public class CriptoYaResponse
    {
        public decimal bid { get; set; }
        public decimal ask { get; set; }
    }

    public class CriptoYaService : ICriptoYaService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public CriptoYaService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<decimal> GetPriceArs(string cryptoCode, string action)
        {
            if (string.IsNullOrEmpty(cryptoCode))
                throw new ArgumentException("cryptoCode es requerido");

            cryptoCode = cryptoCode.ToLower();

            var exchange = _config.GetValue<string>("CriptoYa:Exchange") ?? "satoshitango";

            var url = $"https://criptoya.com/api/{exchange}/{cryptoCode}/ars/1";

            HttpResponseMessage resp;
            try
            {
                resp = await _http.GetAsync(url);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo contactar con CriptoYa: " + ex.Message);
            }

            if (!resp.IsSuccessStatusCode)
            {
                Console.WriteLine($"CriptoYa error {resp.StatusCode}: {url}");
                throw new Exception("No se pudo obtener el precio en este momento. Probá de nuevo en unos segundos.");
            }


            var body = await resp.Content.ReadFromJsonAsync<CriptoYaResponse>();
            if (body == null)
                throw new Exception("Respuesta inválida de CriptoYa");

            return action == "purchase" ? body.ask : body.bid;
        }
    }
}