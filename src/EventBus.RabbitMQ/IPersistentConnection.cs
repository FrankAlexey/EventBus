using RabbitMQ.Client;
using System;

namespace EventBus.RabbitMQ
{
    public interface IPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
