using Confluent.Kafka;
using Kafka;
using System;

namespace GetMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "132.232.27.116:9092",
                GroupId = "yaopeng",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            string text;
            Console.WriteLine("接受中......");
            while ((text = Console.ReadLine()) != "q")
            {

                using (var kafkaProducer = new KafkaConsumer(config, "topic-d"))
                {
                    var result = kafkaProducer.Consume<object>();
                    if (result != null)
                    {
                        Console.WriteLine(result.ToString());
                    }

                }
            }

        }

    }
}
