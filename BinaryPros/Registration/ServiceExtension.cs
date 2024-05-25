using Logic;
using Logic.Abstractions.Cache;
using Logic.Abstractions.Handle;
using Logic.Abstractions.Transport;
using Logic.Abstractions.Transport.Convertor;
using Logic.Abstractions.Transport.Deserializer;
using Logic.Cache;
using Logic.Configuration;
using Logic.Handle;
using Logic.Transport;
using Logic.Transport.Convertor;
using Logic.Transport.Deserializer;
using Models.Common;
using Models.Common.DomainEvents;
using Models.Entity;
using Service.HostesService;

namespace Service.Registration;

public static class ServiceExtension
{
    public static IServiceCollection MainExtension(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
                .HostesService()
                .AddSingleton<IMainProcess, MainProcess>()
                .AddSingleton<ICacheProcess, CacheProcess>()
                .AddSingleton<IRequestSender, RequestSender>()
                .AddSingleton<IMainHandle, MainHandle>()
                .AddSingleton<IDeserializer, Deserializer>()
                .AddSingleton<IConverter<IDomainEvent, byte[]>, DomainEventsConvertor>()
                .AddSingleton<IEntityCache<Identifier<FeedEntity>, FeedEntity>, EntityCache>()
                .AddSingleton<IEventSender, EventSender>()
                .Configure<MainConfiguration>(configuration.GetRequiredSection(nameof(MainConfiguration)))
                .Configure<RequestConfiguration>(configuration.GetRequiredSection(nameof(RequestConfiguration)))
                .Configure<KafkaConfiguration>(configuration.GetRequiredSection(nameof(KafkaConfiguration)));


    private static IServiceCollection HostesService(this IServiceCollection services)
        => services
                .AddHostedService<CacheService>()
                .AddHostedService<ReceiveUpdateService>();
}
