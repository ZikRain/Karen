using DiplomData.Enums;

namespace DiplomData.Entities
{
    public class Product
    {
        public long product_id { get; set; }
        public string product_name { get; set;}
        public ProductType  product_type { get; set; }
        public string product_image { get; set; } 
        public int product_count { get; set; }
        public int product_place1 { get; set; }
        public int product_place2 { get; set; }
        public decimal product_price { get; set; }
        public bool incart { get; set; } = false;
    }
}
