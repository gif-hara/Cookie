using System;
using MessagePipe;

namespace Cookie
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MessageBroker
    {
        public MessageBroker(Action<BuiltinContainerBuilder> builderSetupAction)
        {
            var builder = new BuiltinContainerBuilder();
            builder.AddMessagePipe();
            builderSetupAction?.Invoke(builder);
            var provider = builder.BuildServiceProvider();
            GlobalMessagePipe.SetProvider(provider);
        }
    }
}
