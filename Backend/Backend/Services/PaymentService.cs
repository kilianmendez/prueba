using Backend.Models;
using Microsoft.Extensions.Options;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Backend.Models.Database.Entities;
using Backend.Models.Dtos;

namespace Backend.Services
{
    public class PaymentService
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<PaymentIntentResponse> CreatePaymentIntentAsync(PaymentIntentCreateRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long?)request.Amount,       
                Currency = request.Currency,      
                PaymentMethodTypes = new List<string> { "card"},
            };

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent = await service.CreateAsync(options);

            return new PaymentIntentResponse { ClientSecret = paymentIntent.ClientSecret };
        }

 
    }
}
