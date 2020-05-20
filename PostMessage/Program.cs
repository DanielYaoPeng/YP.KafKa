using Confluent.Kafka;
using Kafka;
using System;

namespace PostMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "132.232.27.116:9092",
                Acks = Acks.All
            };

            using (var kafkaProducer = new KafkaProducer(config, "topic-d"))
            {
                var result = kafkaProducer.Produce<object>("a", new { name = "猪八戒3" }, 1);

            }
            Console.WriteLine("消息发送成功");
            Console.ReadKey();
        }
    }
}
