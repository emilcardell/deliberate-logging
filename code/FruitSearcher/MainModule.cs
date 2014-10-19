using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using NElasticsearch;
using NElasticsearch.Commands;
using Newtonsoft.Json;

namespace FruitSearcher
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ => "Hello World!";

			Get["/search"] = _ => {

				var client = new ElasticsearchRestClient("http://localhost:9200");
				var requestBody = new { 
					query = new { term = new { Name = "banana" } },
				    aggs = new {
						   quantityAggs = new {
								terms = new  { field = "Quantity" }           
				
					   },
					   nameAggs = new {
    							terms = new { field = "Name" }           
				
					   }
					}
 
				};
				var result = client.Search(requestBody, "fruit", null);


				return result;
			};


		}

	}
}