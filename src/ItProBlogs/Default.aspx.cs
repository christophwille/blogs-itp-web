using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace blogs.dotnetgerman.com {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {

            SyndicationFeed feed = (SyndicationFeed)Cache["feed"];

            if (null == feed) {
                XmlReader reader = XmlReader.Create(
                   Server.MapPath(ConfigurationSettings.AppSettings["MetaFeedRssInputPath"]));

                Rss20FeedFormatter rssSerializer = new Rss20FeedFormatter();
                rssSerializer.ReadFrom(reader);
                feed = rssSerializer.Feed;

                Cache.Insert("feed", feed,
                    new CacheDependency(
                        Server.MapPath(ConfigurationSettings.AppSettings["MetaFeedRssInputPath"])));
            }

            this.ListView1.ItemDataBound +=
                new EventHandler<ListViewItemEventArgs>(ListView1_ItemDataBound);
            this.ListView1.DataSource = feed.Items;
            this.ListView1.DataBind();
        }

        void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem) {
                Label NameLabel = (Label)e.Item.FindControl("NameLabel");

                ListViewDataItem currentItem = (ListViewDataItem)e.Item;
                SyndicationItem item = (SyndicationItem)currentItem.DataItem;
                Label TitleLabel = (Label)e.Item.FindControl("TitleLabel");
                if (null != item.Summary) {
                    NameLabel.Text = item.Summary.Text;
                }
                if (null != item.Content) {
                    StringBuilder sbContent = new StringBuilder();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = new System.Text.UTF8Encoding();
                    XmlWriter writer = XmlWriter.Create(sbContent, settings);
                    item.Content.WriteTo(writer, "abc", "default");
                    writer.Flush();
                    string content = sbContent.ToString();
                    content = content.Replace("</abc>", string.Empty);
                    content = content.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?><abc type=\"html\" xmlns=\"default\">", string.Empty);

                    NameLabel.Text = Server.HtmlDecode(content);
                }
                if (item.ElementExtensions.Count > 0) {
                    XmlReader reader = item.ElementExtensions.GetReaderAtElementExtensions();
                    while (reader.Read()) {
                        if ("content:encoded" == reader.Name) {
                            NameLabel.Text = reader.ReadString();
                        }
                    }

                }
                TitleLabel.Text = string.Format("{0}: {1}", item.Authors[0].Name, item.Title.Text);
                HyperLink ItemHyperLink = (HyperLink)e.Item.FindControl("ItemHyperLink");
                if (!(item.Id == null) && item.Id.StartsWith("http://")) {
                    ItemHyperLink.NavigateUrl = item.Id;
                }
                else if (item.Links.Count > 0) {
                    ItemHyperLink.NavigateUrl = item.Links[0].Uri.ToString();
                }
            }
        }
    }
}
