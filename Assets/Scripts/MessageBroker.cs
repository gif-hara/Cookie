using System;
using MessagePipe;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MessageBroker
    {
        public static MessageBroker Instance;
        
        public MessageBroker(Action<BuiltinContainerBuilder> builderSetupAction)
        {
            var builder = new BuiltinContainerBuilder();
            builder.AddMessagePipe();
            builderSetupAction?.Invoke(builder);
            var provider = builder.BuildServiceProvider();
            GlobalMessagePipe.SetProvider(provider);
        }

        public IPublisher<T> GetPublisher<T>()
        {
            return GlobalMessagePipe.GetPublisher<T>();
        }

        public IPublisher<TKey, TValue> GetPublisher<TKey, TValue>()
        {
            return GlobalMessagePipe.GetPublisher<TKey, TValue>();
        }

        public ISubscriber<T> GetSubscriber<T>()
        {
            return GlobalMessagePipe.GetSubscriber<T>();
        }

        public ISubscriber<TKey, TValue> GetSubscriber<TKey, TValue>()
        {
            return GlobalMessagePipe.GetSubscriber<TKey, TValue>();
        }

        public IAsyncPublisher<T> GetAsyncPublisher<T>()
        {
            return GlobalMessagePipe.GetAsyncPublisher<T>();
        }

        public IAsyncPublisher<TKey, TMessage> GetAsyncPublisher<TKey, TMessage>()
        {
            return GlobalMessagePipe.GetAsyncPublisher<TKey, TMessage>();
        }

        public IAsyncSubscriber<T> GetAsyncSubscriber<T>()
        {
            return GlobalMessagePipe.GetAsyncSubscriber<T>();
        }

        public IAsyncSubscriber<TKey, TMessage> GetAsyncSubscriber<TKey, TMessage>()
        {
            return GlobalMessagePipe.GetAsyncSubscriber<TKey, TMessage>();
        }
    }
}
