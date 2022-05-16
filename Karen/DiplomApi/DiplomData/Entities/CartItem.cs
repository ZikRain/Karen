namespace DiplomData.Entities
{
    public class CartItem
    {
        public long cartitem_id { get; set; }
        public long cartitem_cartid { get; set; }
        public long cartitem_productid { get; set; }
        public string cartitem_name { get; set; }
        public decimal cartitem_price { get; set; }
        public int cartitem_quantity { get; set; }
        public decimal amount
        {
            get
            {
                return cartitem_quantity*cartitem_price;
            }
        }
    }
}
