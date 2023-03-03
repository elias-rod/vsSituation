using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;
using Azure.Identity;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostBuilderContext, serviceCollection) =>
    {
        serviceCollection.AddAzureClients(azureClientFactoryBuilder =>
        {
            azureClientFactoryBuilder.AddEventGridPublisherClient(new Uri("https://x-pocif-dev-wus2-1.westus2-1.eventgrid.azure.net/api/events"));
            azureClientFactoryBuilder.AddServiceBusClientWithNamespace("x-pocif-dev-wus2-1.servicebus.windows.net");
            azureClientFactoryBuilder.UseCredential(new DefaultAzureCredential());
        });
    })
    .Build();

host.Run();
