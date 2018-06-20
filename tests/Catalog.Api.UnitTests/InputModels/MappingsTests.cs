using Catalog.Api.InputModels;
using NUnit.Framework;

namespace Catalog.Api.UnitTests.InputModels
{
    public class MappingsTests
    {
        [Test]
        public void InputModel_mappings_is_valid()
        {
            var config = new AutoMapper.MapperConfiguration(
                Mappings.ConfigureInputModelMapping);
            
            config.AssertConfigurationIsValid();
        }
    }
}