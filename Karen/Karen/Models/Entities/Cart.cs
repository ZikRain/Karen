using System.Collections.Generic;

namespace Karen.Models.Entities
{
    public class Cart
    {
        public long cart_id { get; set; }
        public long cart_userid { get; set; }

        public List<CartItem> Items { get; set; }
        public int Count
        {
            get { return Items.Count; }
        }
        public decimal Total 
        {
            get
            {
                decimal total = 0;
                foreach(CartItem item in Items)
                {
                    total += item.amount;
                }
                return total;
            }
        }
    }
}
