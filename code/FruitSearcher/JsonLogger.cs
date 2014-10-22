using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NLog;

namespace FruitSearcher
{
	public class JsonLogger
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public static void LogObject(object obj) {
			logger.Trace(JsonConvert.SerializeObject(obj));			
		}
	}
}