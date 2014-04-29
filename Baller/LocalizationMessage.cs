using System;

namespace SlightlyOrganized.Baller
{
	public class LocalizationMessage
	{
		public string Name { get; set; }
		public string EventType { get; set; }
		public int Id { get; set; }
		public string Data { get; set; }

		public bool IsErrorState
		{
			get
			{
				return EventType.Equals("Error", StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
