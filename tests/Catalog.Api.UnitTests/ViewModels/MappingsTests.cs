using NUnit.Framework;

namespace Catalog.Api.UnitTests.ViewModels
{
    public class MappingsTests
    {
        [Test]
        public void InputModel_mappings_is_valid()
        {
            var config = new AutoMapper.MapperConfiguration(
                ViewModel.Mappings.ConfigureViewModelMapping);
            
            config.AssertConfigurationIsValid();
        }
    }
}