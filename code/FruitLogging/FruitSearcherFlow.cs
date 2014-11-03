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
	public class FruitSearcherFlow : Flow
	{
		
		
		public FruitSearcherFlow ()
		{
			var elasticConfiguration = new ElasticSearchConfiguration();
			elasticConfiguration.IndexNameFormat = @"\a\p\p\l\o\g\s\-yyyyMM";

			CreateProcess()
				.FromInput(new FileInput(@"C:\Repo\deliberate-logging\code\FruitSearcher\logs\*.log", System.Text.Encoding.UTF8, true))
				.Then(new LogJsonParser())
				.Then(new IISDateFix())
				.ToOutput(new ElasticSearchOutput(elasticConfiguration));
			
		}

	}
}
