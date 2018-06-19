using System;

namespace Catalog.Api.DomainModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}