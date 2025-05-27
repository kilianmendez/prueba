namespace Backend.Models.Dtos
{
    public class PaymentIntentCreateRequest
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
