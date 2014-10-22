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
			Get["/"] = _ => View["content/index"];

			Get["/search"] = _ => {

				string quantity = Request.Query.quantity;
				string name = Request.Query.name;
				var client = new ElasticsearchRestClient("http://localhost:9200");

				if(!string.IsNullOrEmpty(quantity) && !string.IsNullOrEmpty(name))
				{
					
					var requestBody = new
					{
						filter = new { @bool = new {
					        must = new object[]
							{  new {term = new { Quantity = quantity }},
					             new {term = new { Name = name.ToLowerInvariant() }}
					        }
					    } },
						aggs = new
						{
							quantityAggs = new
							{
								terms = new { field = "Quantity" }

							},
							nameAggs = new
							{
								terms = new { field = "Name" }

							}
						}

					};
					return client.Search(requestBody, "fruit", null);

				} 
				else if (string.IsNullOrEmpty(quantity) && !string.IsNullOrEmpty(name))
				{
					var requestBody = new
					{
						filter = new { term = new { Name = name.ToLowerInvariant() } },
						aggs = new
						{
							quantityAggs = new
							{
								terms = new { field = "Quantity" }

							},
							nameAggs = new
							{
								terms = new { field = "Name" }

							}
						}

					};
					return client.Search(requestBody, "fruit", null);

				}
				else if (!string.IsNullOrEmpty(quantity) && string.IsNullOrEmpty(name))
				{
					var requestBody = new
					{
						filter = new { term = new { Quantity = quantity } },
						aggs = new
						{
							quantityAggs = new
							{
								terms = new { field = "Quantity" }

							},
							nameAggs = new
							{
								terms = new { field = "Name" }

							}
						}

					};
					return client.Search(requestBody, "fruit", null);

				}
				else
				{
					var requestBody = new
					{
						aggs = new
						{
							quantityAggs = new
							{
								terms = new { field = "Quantity" }

							},
							nameAggs = new
							{
								terms = new { field = "Name" }

							}
						}

					};
					return client.Search(requestBody, "fruit", null);

				}
			};


		}

	}
}