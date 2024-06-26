using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoPriceApp.Models;
using Newtonsoft.Json.Linq;

namespace CryptoPriceApp.Services
{
    public class CryptoPriceService
    {
        private readonly HttpClient _httpClient;

        public CryptoPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CryptoPrice>> GetCryptoPricesAsync(string cryptoId)
        {
            var response = await _httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={cryptoId}&vs_currencies=chf");
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
            {
                return new List<CryptoPrice>();
            }

            var json = JObject.Parse(responseContent);
            var prices = new List<CryptoPrice>();

            if (json[cryptoId] != null && json[cryptoId]["chf"] != null)
            {
                var priceValue = json[cryptoId]["chf"].Value<decimal>();
                prices.Add(new CryptoPrice
                {
                    Time = DateTime.Now,
                    Price = priceValue
                });
            }

            return prices;
        }
    }
}