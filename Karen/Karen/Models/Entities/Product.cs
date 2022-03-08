namespace Karen.Models.Entities
{
    public class Product
    {
        public long product_id { get; set; }
        public string product_name { get; set;}
        public int product_count { get; set; }
        public string product_place { get; set; }
        public decimal product_price { get; set; }
    }
}
