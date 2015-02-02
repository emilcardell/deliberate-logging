using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using NElasticsearch;
using NElasticsearch.Commands;
using NElasticsearch.Models;

namespace FruitSearcher
{
	public class FruitSearcher
	{
		public static SearchResponse<Fruit> FindFruit(string name, string quantity)
		{
			var client = new ElasticsearchRestClient("http://localhost:9200");
			SearchResponse<Fruit> result;

			if(!string.IsNullOrEmpty(name) && name.ToLowerInvariant() == "banana")
			{
				Thread.Sleep(2000);
			}

			if(!string.IsNullOrEmpty(quantity) && quantity == "10")
			{
				JsonLogger.LogErrorObject(new { FruitName = name, Quantity = quantity, EventName = "FruitSearchError"});
				throw new Exception("Something went horribly wrong!");
			}

			if(!string.IsNullOrEmpty(quantity) && !string.IsNullOrEmpty(name))
			{

				var requestBody = new
				{
					size = 10000,
					filter = new
					{
						@bool = new
						{
							must = new object[]
							{  new {term = new { Quantity = quantity }},
					             new {term = new { Name = name.ToLowerInvariant() }}
					        }
						}
					},
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
				result = client.Search<Fruit>(requestBody, "fruit", null);

			}
			else if(string.IsNullOrEmpty(quantity) && !string.IsNullOrEmpty(name))
			{
				var requestBody = new
				{
					size = 10000,
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

				result = client.Search<Fruit>(requestBody, "fruit", null);

			}
			else if(!string.IsNullOrEmpty(quantity) && string.IsNullOrEmpty(name))
			{
				var requestBody = new
				{
					size = 10000,
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
				result = client.Search<Fruit>(requestBody, "fruit", null);

			}
			else
			{
				var requestBody = new
				{
					size = 10000,
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
				result = client.Search<Fruit>(requestBody, "fruit", null);

			}

			return result;
		}
	}
}