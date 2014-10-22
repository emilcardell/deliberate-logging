using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace FruitDelivery
{
	public class JsonLogger
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public static void LogObject(object obj)
		{
			logger.Trace(JsonConvert.SerializeObject(obj));
		}
	}
}
