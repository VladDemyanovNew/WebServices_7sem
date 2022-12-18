using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Lab7.SyndicationClient.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            var request = WebRequest.Create("http://localhost:40001/SyndicationService/WsDvrFeed/format=rss");
            var response = request.GetResponse();
            var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            this.ViewBag.Feed = content;
            return View();
        }
    }
}