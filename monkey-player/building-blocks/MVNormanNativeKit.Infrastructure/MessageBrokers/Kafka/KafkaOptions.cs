﻿using Confluent.Kafka;

namespace MVNormanNativeKit.Infrastructure.MessageBrokers.Kafka
{
    public class KafkaOptions
    {
        public ConsumerConfig Consumer { get; set; }
        public ProducerConfig Producer { get; set; }
        public string Topic { get; set; }
        public string Topics { get; set; }
    }
}
