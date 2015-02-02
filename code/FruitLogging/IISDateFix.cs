using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogFlow;

namespace FruitLogging
{
	public class IISDateFix : LogProcessor
	{
		public override Result Process(Result result)
		{
			var value = result.EventTimeStamp.Value;
			var dateResult = value.AddMilliseconds(5);
			result.EventTimeStamp = dateResult;
			return result;
		}
	}
}
