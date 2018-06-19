namespace Catalog.Api.DomainModel
{
    public struct PriceValidator
    {
        private readonly decimal _price;
        private readonly bool _confirmed;
        
        public PriceValidator(decimal price, bool confirmed)
        {
            _price = price;
            _confirmed = confirmed;
        }

        public bool Validate()
        {
            if (_price < 0)
            {
                return false;
            }
            
            if (_confirmed)
            {
                return true;
            }

            return _price <= 999;
        }
    }
}