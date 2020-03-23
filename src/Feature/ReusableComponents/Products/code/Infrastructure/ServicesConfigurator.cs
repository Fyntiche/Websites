using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Repositories;
using Websites.Foundation.DependencyInjection;

namespace Websites.Feature.ReusableComponents.Products.Infrastructure
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllersInCurrentAssembly();
            serviceCollection.AddSingleton<ICatalogRepository, CatalogRepository>();
        }
    }
}