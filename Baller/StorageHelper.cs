using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlightlyOrganized.Baller
{
	public class StorageHelper
	{
		private const string StorageKey = "Baller.LocalizationNotificationListener";

		public static string GetSessionStorageKey()
		{
			return StorageKey + "." + HttpContext.Current.Session.SessionID;
		}
	}
}
