using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SlightlyOrganized.Baller
{
    public class LocalizationNotificationListener : TraceListener
    {
		public TimeSpan CacheDuration { get; set; }

		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			var localData = data.ToString();

			// Try and match data to a TraceRecord and so we an write better output. ToString() on data is putting the running app info on.
			// possible future enhancement of breaking it back to trace record and writing more discrete fields
			try
			{
				var navigator = data as XPathNavigator;
				if (navigator != null)
				{
					var xElement = navigator.UnderlyingObject as XElement;

					if (xElement != null)
					{
						var singleOrDefault = xElement.Elements().SingleOrDefault(a => a.Name.LocalName == "Description");
						if (singleOrDefault != null)
						{
							localData = singleOrDefault.Value;
						}
					}
				}
			}
			catch (Exception)
			{
				//reset
				localData = data.ToString();
			}

			var storage = new Storage(StorageHelper.GetSessionStorageKey());

			// Store the info in http runtime cache
			storage.Write(new LocalizationMessage
			{
				Name = source,
				EventType = eventType.ToString(),
				Id = id,
				Data = localData
			});
		}

	    public override void Write(string message)
	    {
		    throw new NotImplementedException();
	    }

	    public override void WriteLine(string message)
	    {
		    throw new NotImplementedException();
	    }
    }
}
