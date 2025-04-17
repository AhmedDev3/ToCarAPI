namespace ToCarAPI.DTOs
{
    public class CheckoutRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Location1 { get; set; } = string.Empty;
        public string Location2 { get; set; } = string.Empty;
    }
}