using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogFlow;
using LogFlow.Builtins.Inputs;
using LogFlow.Builtins.Outputs;

namespace FruitLogging
{
	public class FruitDeliveryFlow : Flow
	{
		public FruitDeliveryFlow()
		{


			var elasticConfiguration = new ElasticSearchConfiguration();
			elasticConfiguration.IndexNameFormat = @"\d\e\l\i\v\e\r\y\-yyyyMM";

			CreateProcess()
				.FromInput(new FileInput(@"C:\Repo\deliberate-logging\code\FruitDelivery\bin\Debug\logs\*.log", System.Text.Encoding.UTF8, true))
				.Then(new LogJsonParser())
				.ToOutput(new ElasticSearchOutput(elasticConfiguration));
		}
	}
}
