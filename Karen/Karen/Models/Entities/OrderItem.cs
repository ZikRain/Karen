namespace Karen.Models.Entities
{
    public class OrderItem
    {
        public long order_item_id { get; set; }
        public long order_item_order_id { get; set; }
        public long order_item_product_id { get; set; }
        public string order_item_name { get; set; }
        public decimal order_item_price { get; set;}
        public int order_item_quantity { get; set;}
    }
}
