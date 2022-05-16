using Diplom.API.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Diplom.API.AdditionalModel
{
    public class CategoryData: ObservableCollection<Product>
    {
       public long id { get; set; }
       public string name { get; set; }
       public CategoryData(string Name,long Id, ObservableCollection<Product> Products) : base(Products)
        {
            name = Name;
            id = Id;
        }
    }
}
