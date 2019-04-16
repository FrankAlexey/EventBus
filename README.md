# Light EventBus 

Light EventBus is a simple lightweight .NET Core library that allows developers to use:
* easy in-memory event-dispatching
* publishing events to RabbitMQ
* consume events via event handlers

## NuGet packages

- [Light.EventBus](https://www.nuget.org/packages/Light.EventBus/)
- [Light.EventBus.RabbitMQ](https://www.nuget.org/packages/Light.EventBus.RabbitMQ/)

### Installing via NuGet

You can install base abstractions library to use [InMemorySubscriptionManager](https://github.com/FrankAlexey/EventBus/blob/master/src/EventBus/InMemorySubscriptionsManager.cs)

    Install-Package Light.EventBus
    
Or install RabbitMQ-based implementation of Event Bus
    
    Install-Package Light.EventBus.RabbitMQ
    
## Quick start:

### RabbitMQ-based implementation of Event Bus in ASP.NET Core project

1. Install [Light.EventBus.RabbitMQ](https://www.nuget.org/packages/Light.EventBus.RabbitMQ/) NuGet in your application

2. Configure and register event bus using Dependency Injection

```csharp
   public IServiceProvider ConfigureServices(IServiceCollection services)
   {
        ...
        services.AddEventBusRabbitMQ(() =>
        {
            var rabbitSection = Configuration.GetSection("RabbitMQ");

            return new RabbitMQOptions()
            {
                UserName = rabbitSection["UserName"],
                Password = rabbitSection["Password"],
                HostName = rabbitSection["HostName"],
                Port = Int32.Parse(rabbitSection["Port"]),
                QueueName = rabbitSection["QueueName"],
                ExchangeName = rabbitSection["ExchangeName"],
                RetryCount = rabbitSection["RetryCount"]
            };
        });
        ...
   }
```

3. Create an event class

```csharp
    public class MyIntegrationEvent : IntegrationEvent
    {      
      //Properties
    }  
```

4. Use [IEventBus](https://github.com/FrankAlexey/EventBus/blob/master/src/EventBus/Abstractions/IEventBus.cs) for publishing events
```csharp
    var eventBus = serviceProvider.GetRequiredService<IEventBus>();
    eventBus.Publish(new MyIntegrationEvent());
```

5. For consuming events create an event handler class
```csharp
    public class MyIntegrationEventHandler : IIntegrationEventHandler<MyIntegrationEvent>
    {
        private readonly Dependency _anyDependency;

        public MyIntegrationEventHandler(Dependency anyDependency)
        {
            ...
        }

        public async Task Handle(MyIntegrationEvent @event)
        {
            //Process your event
            await Task.CompletedTask;            
        }
    }
```

6. Subscribe your event handler via [IEventBus](https://github.com/FrankAlexey/EventBus/blob/master/src/EventBus/Abstractions/IEventBus.cs)

```csharp
    var eventBus = serviceProvider.GetRequiredService<IEventBus>();
    eventBus.Subscribe<MyIntegrationEvent, MyIntegrationEventHandler>();
```

Any contributions or comments are welcome!
