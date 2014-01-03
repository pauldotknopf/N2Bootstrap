using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using N2;
using N2.Definitions;
using N2.Engine.Globalization;
using N2.Persistence.Search;
using N2.Web;
using N2.Web.Mvc;
using N2Bootstrap.Library.Models;

namespace N2Bootstrap.Library.Controllers
{
	public class StartPageController : ContentController<StartPage>
    {
		public ActionResult NotFound()
		{
			var closestMatch = Content.Traverse.Path(Request.AppRelativeCurrentExecutionFilePath.Trim('~', '/')).StopItem;
			
			var startPage = Content.Traverse.ClosestStartPage(closestMatch);
			var urlText = Request.AppRelativeCurrentExecutionFilePath.Trim('~', '/').Replace('/', ' ');
			var similarPages = GetSearchResults(startPage, urlText, 10).ToList();

			ControllerContext.RouteData.ApplyCurrentPath(new PathData(new ContentPage { Parent = startPage }));
			Response.TrySkipIisCustomErrors = true;
			Response.Status = "404 Not Found";

			return View(similarPages);
		}

		private IEnumerable<ContentItem> GetSearchResults(ContentItem root, string text, int take)
		{
			var query = Query.For(text).Below(root).ReadableBy(User, Roles.GetRolesForUser).Except(Query.For(typeof(ISystemNode)));
			var hits = Engine.Resolve<ITextSearcher>().Search(query).Hits.Select(h => h.Content);
			return hits;
		}
	}
}
