using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogFlow;
using LogFlow.Builtins.Inputs;
using LogFlow.Builtins.Outputs;
using LogFlow.Builtins.Processors.IISLog;


namespace FruitLogging
{
	public class IisFlow : Flow
	{
		public IisFlow()
		{
			var elasticConfiguration = new ElasticSearchConfiguration();
			elasticConfiguration.IndexNameFormat = @"\i\i\s\l\o\g\s\-yyyyMM";

			CreateProcess()
				.FromInput(new FileInput(@"C:\inetpub\logs\LogFiles\W3SVC1\*.log", System.Text.Encoding.UTF8, true))
				.Then(new IISLogProcessor())
				.ToOutput(new ElasticSearchOutput(elasticConfiguration));
		}
	}
}
