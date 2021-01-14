﻿using RawRabbit.Configuration;

namespace MVNormanNativeKit.Infrastructure.MessageBrokers.RabbitMQ
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public new QueueOptions Queue { get; set; }
        public new ExchangeOptions Exchange { get; set; }
    }

    public class QueueOptions : GeneralQueueConfiguration
    {
        public string Name { get; set; }
    }

    public class ExchangeOptions : GeneralExchangeConfiguration
    {
        public string Name { get; set; }
    }
}