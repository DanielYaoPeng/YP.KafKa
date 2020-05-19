using Confluent.Kafka;
using Kafka;
using System;
using Xunit;

namespace KafkaTest
{
    public class UnitTest1
    {
        [Fact]
        public void SendMessage()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "132.232.27.116:9092",
                Acks = Acks.All
            };

            using (var kafkaProducer = new KafkaProducer(config, "topic-b"))
            {
                var result = kafkaProducer.Produce<object>("a", new { name = "÷Ì∞ÀΩ‰3" }, 1);

            }
        }

        [Fact]
        public void GetMessage()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "132.232.27.116:9092",
                GroupId = "yaopeng",
                AutoOffsetReset = AutoOffsetReset.Error

            };

            using (var kafkaProducer = new KafkaConsumer(config, "topic-b"))
            {
                var result = kafkaProducer.Consume<object>();

                Console.WriteLine(result.ToString());
            }
        }

    }
}
