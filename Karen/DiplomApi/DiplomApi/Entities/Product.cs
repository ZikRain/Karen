using System;
using DiplomData.Enums;

namespace DiplomApi.Entities
{
    public class Product
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public int Place1 { get; set; }
        public int Plcae2 { get; set; }
        public decimal Price { get; set; }
    }
}
