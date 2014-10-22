using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using NLog;

namespace FruitDelivery
{
	class Program
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
		{
			var fruits = new List<string> { "Banana", "Kiwi", "Orange", "Apple", "Pear" };
			var rand = new Random();
			var client = new ElasticsearchClient(); 


			while(true)
			{
				var fruit = new FruitDelivery()
				{
					Id = Guid.NewGuid().ToString(),
					Name = fruits[rand.Next(0, fruits.Count())],
					Quantity = rand.Next(1, 20)
				};

				var indexResponse = client.Index("fruit","fruit",fruit.Id, fruit);

				JsonLogger.LogObject(new { FruitName = fruit.Name, Quantity = fruit.Quantity, ClientName = "FruitDelivery" });

				Thread.Sleep(1000);
			}
		}
	}
}
