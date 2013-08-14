using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace TDJ.Alarm.SocketIO.Worker
{
    public class QueueConventions : IConventions
    {
        public ErrorExchangeNameConvention ErrorExchangeNamingConvention { get; set; }
        public ErrorQueueNameConvention ErrorQueueNamingConvention { get; set; }
        public ExchangeNameConvention ExchangeNamingConvention { get; set; }

        public QueueNameConvention QueueNamingConvention { get; set; }
        public RpcExchangeNameConvention RpcExchangeNamingConvention { get; set; }
        public RpcRoutingKeyNamingConvention RpcRoutingKeyNamingConvention { get; set; }
        public TopicNameConvention TopicNamingConvention { get; set; }

        public QueueConventions()
        {
            ExchangeNamingConvention = messageType => string.Format("FM.Ex.{0}", messageType.Name);
            ErrorExchangeNamingConvention = messageType => "Panel.Ex.Error";

            TopicNamingConvention = messageType => "";

            QueueNamingConvention = (messageType, subscriptionId) =>
            {
                if (string.IsNullOrEmpty(subscriptionId))
                {
                    return string.Format("Panel.Q.{0}", messageType.Name);
                }
                else
                {
                    return string.Format("Panel.Q.{0}.{1}", messageType.Name, subscriptionId);
                }
            };

            ErrorQueueNamingConvention = () => "Panel.Q.Error";

            RpcExchangeNamingConvention = () => "Panel.Ex.RPC";

            RpcExchangeNamingConvention = () => "RPC.";
        }
    }
}
