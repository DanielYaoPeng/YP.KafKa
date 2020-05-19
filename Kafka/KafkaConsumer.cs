using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kafka
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private bool disposeHasBeenCalled = false;
        private readonly object disposeHasBeenCalledLockObj = new object();

        private readonly IConsumer<string, string> _consumer;

        /// <summary>
        /// 构造函数，初始化配置
        /// </summary>
        /// <param name="config">配置参数</param>
        /// <param name="topic">主题名称</param>
        public KafkaConsumer(ConsumerConfig config, string topic)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();

            _consumer.Subscribe(topic);
        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <returns></returns>
        public T Consume<T>() where T : class
        {
            try
            {
                var result = _consumer.Consume(TimeSpan.FromSeconds(1));
                if (result != null)
                {
#if DEBUG
                    Console.WriteLine($"Topic: {result.Topic} Partition: {result.Partition} Offset: {result.Offset} Value:{result.Value}");
#endif

                    if (typeof(T) == typeof(string))
                        return (T)Convert.ChangeType(result.Value, typeof(T));

                    return JsonConvert.DeserializeObject<T>(result.Value);
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"consume error: {e.Error.Reason}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"consume error: {e.Message}");
            }

            return default;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            lock (disposeHasBeenCalledLockObj)
            {
                if (disposeHasBeenCalled) { return; }
                disposeHasBeenCalled = true;
            }

            if (disposing)
            {
                _consumer?.Close();
            }
        }
    }
}
