using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe.Checkout;
using ZnanyTrener.API.Interfaces;

namespace ZnanyTrener.API.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<Session> CreateSessionAsync()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = 15000,
                            Currency = "pln",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donation"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/payment-success",
                CancelUrl = "http://localhost:4200/payment-fail",
            };
            
            
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }
    }
}