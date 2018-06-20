namespace Catalog.Api.InputModels
{
    public class AddProductInputModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public bool ConfirmPrice { get; set; }
    }
    
    public class UpdateProductInputModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public bool ConfirmPrice { get; set; }
    }
}