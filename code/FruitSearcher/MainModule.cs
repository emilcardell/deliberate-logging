using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elasticsearch.Net;
using Nancy;

namespace FruitSearcher
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ => "Hello World!";

			Get["/firstResult"] = _ => {
				var client = new ElasticsearchClient(); 
				client.Search("fruit", {})

			};
		}

	}
}