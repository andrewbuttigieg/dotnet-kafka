
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

      using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
      {
        string text = null;

        while (text != "exit")
        {
            Console.WriteLine("Type a phrase and press [Enter] to publish:");
          text = Console.ReadLine();                          
          Console.WriteLine($"Publishing {text}");                                                                                                                                    
          producer.ProduceAsync("hello-topic", null, text);
        }

        producer.Flush(100);
      }
    }
  }
}