//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportClubSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Nullable<int> quntity { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public void Add(Product p)
        {
            db.Products.Add(p);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
