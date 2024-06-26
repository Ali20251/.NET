using Microsoft.AspNetCore.Mvc;
using CryptoPriceApp.Services;
using System.Threading.Tasks;

namespace CryptoPriceApp.Controllers
{
    public class CryptoController : Controller
    {
        private readonly CryptoPriceService _cryptoPriceService;

        public CryptoController(CryptoPriceService cryptoPriceService)
        {
            _cryptoPriceService = cryptoPriceService;
        }

        public async Task<IActionResult> Index()
        {
            var cryptoId = "bitcoin"; // Replace with the desired cryptocurrency ID
            var prices = await _cryptoPriceService.GetCryptoPricesAsync(cryptoId);
            if (prices == null)
            {
                return View(new List<CryptoPriceApp.Models.CryptoPrice>());
            }
            return View(prices);
        }
    }
}