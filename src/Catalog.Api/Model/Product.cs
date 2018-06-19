using System;

namespace Catalog.Api.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}