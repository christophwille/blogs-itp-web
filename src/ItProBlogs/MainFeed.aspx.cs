using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace blogs.dotnetgerman.com {
	public partial class MainFeed : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Status = "301 Moved Permanently";
			Response.AddHeader("Location", String.Format("{0}", ResolveUrl("~/" + ConfigurationSettings.AppSettings["MetaFeedRssUri"])));
			Response.End();
		}
	}
}
