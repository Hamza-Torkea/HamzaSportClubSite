using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Bill { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}