using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

using WsDvrModel;

namespace Lab7.SyndicationServiceLibrary
{
    public class WsDvrFeed : IWsDvrFeed
    {
        public SyndicationFeedFormatter CreateFeed()
        {
            var feed = new SyndicationFeed("WsDvr Feed", "A WCF Syndication Feed", null);
            feed.Items = this.GetNewsItems();

            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }

            return formatter;
        }

        private List<SyndicationItem> GetNewsItems()
        {
            var uri = new Uri("http://localhost:53215/Services/WsDvr.svc");
            var wsDvrService = new WsDvrEntities(uri);
            var feedItems = new List<SyndicationItem>();

            DataServiceQuery<Marks> marks = wsDvrService.Marks.Expand("Students");
            foreach (var mark in marks)
            {
                var title = $"{mark.Subject} - {mark.Students.Name}";
                var content = $"Оценка {mark.Value}";
                var itemId = mark.Id.ToString();

                var feedItem = new SyndicationItem(title, content, new Uri($"http://localhost/{itemId}"), itemId, DateTime.Now);
                feedItems.Add(feedItem);
            }

            return feedItems;
        }
    }
}
