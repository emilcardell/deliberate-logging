using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace FruitSearcher
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ => "Hello World!";
		}

	}
}