using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlightlyOrganized.Baller
{
	public class BallerHelper
	{
		private static readonly Func<LocalizationMessage, string> RenderItem = x =>
			@"<div class='bocce-result @bocceError'>
				<!--span class='bocce-result-name'>@name</span>
				<span class='bocce-result-id'>@id</span>
				<span class='bocce-result-eventType'>@eventType</span-->
				<span class='bocce-result-data'>@data</span>
			</div>".Replace("@name", HttpUtility.HtmlEncode(x.Name))
				   .Replace("@id", HttpUtility.HtmlEncode(x.Id))
				   .Replace("@eventType", HttpUtility.HtmlEncode(x.EventType))
				   .Replace("@data", HttpUtility.HtmlEncode(x.Data))
				   .Replace("@bocceError", x.IsErrorState ? " bocce-error" : "");

		private const string CssClasses = @"
			<style type='text/css'>
				.bocce-results {
					color: black;
					text-align: center;
					border: 1px solid black;
					background-color: white;
					display: block;
					font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
					font-size: 13px;
					right: 0px;
					position: fixed;
					top: 0px;
					width: 120px;
					z-index: 2147483643;
				}

				.bocce-results-label {}

				.bocce-results-list {
					display: none;
					overflow-y: scroll;
					height: 500px;
					position: fixed;
					right: 0px;
					box-shadow: -5px 5px 20px #888888;
				}

				.bocce-error { background-color: darkred !important; color: white !important; }

				.bocce-result {
					color: white;
					background-color: darkgreen;
					display: block;
					font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
					font-size: 13px;
					right: initial;
					top: initial;
					z-index: 2147483643;
					border: 1px solid black;
				}
			</style>
		";

		public static string Render()
		{
			var storage = new Storage(StorageHelper.GetSessionStorageKey());
			var list = storage.GetStorage();

			var errorStateCss = list.Any(x => x.IsErrorState) ? " bocce-error" : "";

			var builder = new StringBuilder();
			builder.AppendLine(CssClasses);
			builder.Append("<div class='bocce-results").Append(errorStateCss).Append("'>")
				   .AppendLine("<div class='bocce-results-label'>Bocce Log</div>")
			       .AppendLine("<div class='bocce-results-list'>");
			list.ForEach(x => builder.AppendLine(RenderItem(x)));
			builder.AppendLine("</div></div>");
			builder.AppendLine("<script type='text/javascript'>$(function() { $('.bocce-results .bocce-results-label').click(function() { $('.bocce-results .bocce-results-list').toggle(); }); });</script>");

			storage.Clear();

			return builder.ToString();
		}
	}
}
