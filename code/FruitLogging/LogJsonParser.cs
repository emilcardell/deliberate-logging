using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogFlow;
using Newtonsoft.Json.Linq;

namespace FruitLogging
{
	public class LogJsonParser : LogProcessor
	{
		public override Result Process(Result result)
		{
			var logLine = result.Line;
			var timestampPosition = logLine.IndexOf(" ", logLine.IndexOf(" ") + 1);
			var eventTimeStamp = DateTime.Parse(logLine.Substring(0, timestampPosition).Trim(), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
			var logLevelPosition = logLine.IndexOf(" ", timestampPosition + 1);
			var json = logLine.Substring(logLine.IndexOf(" ", logLevelPosition)).Trim();

			result.EventTimeStamp = eventTimeStamp;

			var jObject = JObject.Parse(json);
			foreach(var childNode in jObject.Children())
			{
				var childProperty = childNode as JProperty;
				result.Json.Add(childNode);
			}

			return result;
		}

		
	}
}
