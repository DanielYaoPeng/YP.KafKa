using System;

namespace Kafka
{
    /// <summary>
    /// 消费者
    /// </summary>
    public interface IKafkaConsumer : IDisposable
    {
        /// <summary>
        /// 消费数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Consume<T>() where T : class;
    }
}
