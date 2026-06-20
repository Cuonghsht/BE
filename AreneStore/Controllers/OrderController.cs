using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AreneStore.Enity;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AreneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Tele _tele;
        public OrderController(IOptions<Tele> teleOptions)
        {
            _tele = teleOptions.Value;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            using var client = new HttpClient();
            var Bottoken = _tele.BotToken;
            var ChatId = _tele.ChatId;
            var Mess = $"FullName: {order.FullName}\n" +
                          $"Address: {order.Address}\n" +
                          $"PhoneNumber: {order.PhoneNumber}\n" +
                          $"Quantity: {order.Quantity}\n" +
                          $"Price: {order.Price}";
            var builderMess = Uri.EscapeDataString(Mess);

            var url = $"https://api.telegram.org/bot{Bottoken}/sendMessage" +
                      $"?chat_id={ChatId}&text={builderMess}";
            await client.GetAsync(url);

            return Ok(new
            {
                message = "Đã gửi Telegram",
                data = order
            });

          
        }
    }
}
