namespace appServer.Models
{
    public class Product
    {
        public int id { get; set; }
        public string? ProductName { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string? CurrencyType { get; set; }
    }
}

