using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;
using Azure.Identity;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostBuilderContext, serviceCollection) =>
    {
        serviceCollection.AddAzureClients(azureClientFactoryBuilder =>
        {
            azureClientFactoryBuilder.AddEventGridPublisherClient(new Uri("https://evgt-pocif-dev-wus2-1.westus2-1.eventgrid.azure.net/api/events"));
            azureClientFactoryBuilder.UseCredential(new DefaultAzureCredential());
        });
    })
    .Build();

host.Run();
