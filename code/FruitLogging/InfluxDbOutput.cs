using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Net;
using InfluxDB.Net.Models;
using LogFlow;

namespace FruitLogging
{
	public class InfluxDbOutput : LogProcessor
	{
		public override Result Process(Result result)
		{
			if(!result.EventTimeStamp.HasValue)
				return result;

			var t = result.EventTimeStamp.Value - new DateTime(1970, 1, 1);
			var secondsSinceEpoch = (long)t.TotalMilliseconds;

			var _client = new InfluxDb("http://192.168.33.101:8086", "root", "root");
			Serie serie = new Serie.Builder("iislog")
							.Columns("status", "query", "time")
							.Values(result.Json.Value<int>("sc-status").ToString(), result.Json.Value<string>("cs-uri-query"), secondsSinceEpoch)
							.Build();
			Task.Run(() => _client.WriteAsync("graphana", TimeUnit.Milliseconds, serie));

			return result;
		}
	}

	public class InfluxDbOutputForFruitSearcher : LogProcessor
	{
		public override Result Process(Result result)
		{
			if(!result.EventTimeStamp.HasValue)
				return result;

			var t = result.EventTimeStamp.Value - new DateTime(1970, 1, 1);
			var secondsSinceEpoch = (long)t.TotalMilliseconds;

			var _client = new InfluxDb("http://192.168.33.101:8086", "root", "root");
			Serie serie = new Serie.Builder("iislog")
							.Columns("FruitName", "Quantity", "time")
							.Values(result.Json.Value<string>("FruitName").ToString(), result.Json.Value<string>("Quantity"), secondsSinceEpoch)
							.Build();
			Task.Run(() => _client.WriteAsync("graphana", TimeUnit.Milliseconds, serie));

			return result;
		}
	}
}
