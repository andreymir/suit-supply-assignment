using Catalog.Api.ViewModels;
using NUnit.Framework;

namespace Catalog.Api.UnitTests.ViewModels
{
    public class MappingsTests
    {
        [Test]
        public void InputModel_mappings_is_valid()
        {
            var config = new AutoMapper.MapperConfiguration(
                Mappings.ConfigureViewModelMapping);
            
            config.AssertConfigurationIsValid();
        }
    }
}