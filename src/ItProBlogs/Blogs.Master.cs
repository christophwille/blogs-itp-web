﻿using System;
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
	public partial class Blogs : System.Web.UI.MasterPage {
		protected void Page_Load(object sender, EventArgs e)
        {
			this.RssHyperlink.NavigateUrl =
				ConfigurationSettings.AppSettings["MetaFeedRssUri"];
			this.EnableViewState = false;
        }
	}
}
