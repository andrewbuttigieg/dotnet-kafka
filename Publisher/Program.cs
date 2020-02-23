
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
        { "bootstrap.servers", "127.0.0.1:9092" }
      };

      using (var producer = new Producer<int, string>(config, new IntSerializer(), new StringSerializer(Encoding.UTF8)))
      {
        string text = null;
        Random random = new Random();
        while (text != "exit")
        {
          Console.WriteLine("Type a phrase and press [Enter] to publish:");
          text = Console.ReadLine();   
          for(int i = 0; i < 1000; i ++)                       
          {
            int key = (int)(random.NextDouble() * 255);
            Console.WriteLine($"Publishing {text} for key {key}");
            producer.ProduceAsync("messaging-topic", key, text);
          }
        }

        producer.Flush(100);
      }
    }
  }
}