using NUnit.Framework;

namespace Catalog.Api.UnitTests.InputModel
{
    public class MappingsTests
    {
        [Test]
        public void InputModel_mappings_is_valid()
        {
            var config = new AutoMapper.MapperConfiguration(
                Catalog.Api.InputModel.Mappings.ConfigureInputModelMapping);
            
            config.AssertConfigurationIsValid();
        }
    }
}