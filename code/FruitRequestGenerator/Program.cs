using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace FruitRequestGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			var fruits = new List<string> { "", "Banana", "Kiwi", "Orange", "Apple", "Pear", "Mango", "Pineapple", "Carrot" };
			var rand = new Random();

			while(true)
			{
				var client = new RestClient("http://localhost:5050");

				var name = fruits[rand.Next(0, fruits.Count())];
				var quantity = rand.Next(0, 22);

				var request = new RestRequest("search", Method.GET);
				if(!string.IsNullOrEmpty(name)){
					request.AddParameter("name", name);
				}

				if(quantity > 0){
					request.AddParameter("quantity", quantity.ToString());
				}
				
				var response = client.Execute(request);
				Console.WriteLine(response.Content);
				Thread.Sleep(1000);
			}
		}
	}
}
