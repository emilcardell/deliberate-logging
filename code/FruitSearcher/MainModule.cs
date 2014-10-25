using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Nancy;
using Newtonsoft.Json;

namespace FruitSearcher
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ => View["content/index"];

			Get["/search"] = _ => {

				string quantity = Request.Query.quantity;
				string name = Request.Query.name;
				
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var result = FruitSearcher.FindFruit(name, quantity);				

				stopWatch.Stop();
				long duration = stopWatch.ElapsedMilliseconds;
				JsonLogger.LogObject(new { QueryDuration = duration, FruitName = name, Quantity = quantity, ClientName = "FruitSearcher", Hits = result.hits.hits.Count() });

				return Response.AsJson(result);
			};


		}

	}
}