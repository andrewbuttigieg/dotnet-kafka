﻿
    using System;
  using System.Collections.Generic;
  using System.Text;
  using Confluent.Kafka;
  using Confluent.Kafka.Serialization;

namespace dotnet_kafka
{
  public class Program
  {
    static void Main(string[] args)
    {
      var config = new Dictionary<string, object>
      {
          { "group.id", "sample-consumer"},//+Guid.NewGuid().ToString() },
          { "bootstrap.servers", "127.0.0.1:9092" },
          { "enable.auto.commit", "false"}
      };

      using (var consumer = new Consumer<int, string>(config, new IntDeserializer(), new StringDeserializer(Encoding.UTF8)))
      {                
        consumer.Subscribe(new string[]{"messaging-topic"});

        consumer.OnMessage += (_, msg) => 
        {
          Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} Key:{msg.Key} Value:{msg.Value}");
          consumer.CommitAsync(msg);
        };

        while (true)
        {
            consumer.Poll(100);
        }
      }
    }
  }
}
