using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GenericServiceProvider
{
    public class GenericServiceProviderBuilder
    {
        private IConfiguration Configuration { get; set; }

        private List<Action<IConfiguration, IServiceCollection>> _serviceConfigurationDelegates = new();

        public GenericServiceProviderBuilder ConfigureServices(Action<IConfiguration, IServiceCollection> configureDelegate)
        {
            _serviceConfigurationDelegates.Add(configureDelegate);
            return this;
        }
        
        //  TODO: Queue this.
        public GenericServiceProviderBuilder AddConfiguration(params string[] configFiles)
        {
            var configBuilder =
                new ConfigurationBuilder();
            foreach (var json in configFiles)
            {
                configBuilder.AddJsonFile(json);
            }

            
            Configuration = configBuilder.Build();

            return this;
        }

        public IServiceProvider Build()
        {
            var services = new ServiceCollection();
            foreach (var serviceDelegate in _serviceConfigurationDelegates)
            {
                serviceDelegate(Configuration, services);
            }

            return services.BuildServiceProvider();
        }
    }
}

