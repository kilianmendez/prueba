using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly StripeSettings _stripeSettings;
        private PaymentService _paymentService;

        public PaymentController(IOptions<StripeSettings> stripeSettings, PaymentService paymentService)
        {
            _stripeSettings = stripeSettings.Value;
            _paymentService = paymentService;
        }

        [HttpPost("CreatePaymentIntent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentIntentCreateRequest request)
        {
            try
            {
                var response = await _paymentService.CreatePaymentIntentAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
