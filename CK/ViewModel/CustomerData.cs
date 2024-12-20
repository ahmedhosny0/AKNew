namespace CK.ViewModel
{
    public class CustomerData :ConnectionDB
    {
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Phonenumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
