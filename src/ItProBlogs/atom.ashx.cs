using System;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using System.Xml;

namespace blogs.dotnetgerman.com {
	/// <summary>
	/// Summary description for $codebehindclassname$
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class atom : IHttpHandler {

		public void ProcessRequest(HttpContext context)
		{
			XmlDocument doc = (XmlDocument)context.Cache["doc"];
			if (doc == null) {
				XmlTextReader xml =
					new XmlTextReader(
						context.Server.MapPath(
						ConfigurationSettings.AppSettings["MetaFeedAtomInputPath"]));

				xml.Read();
				doc = new XmlDocument();
				doc.Load(xml);
				doc.RemoveChild(doc.FirstChild);
				context.Cache.Insert("doc", doc, new CacheDependency(context.Server.MapPath(ConfigurationSettings.AppSettings["MetaFeedAtomInputPath"])));
				context.Cache["etag"] = Guid.NewGuid();
				xml.Close();
			}

			string ownETag = context.Cache["etag"].ToString();
			string clientETag = context.Request.Headers.Get("If-None-Match");
			context.Response.Cache.SetCacheability(HttpCacheability.Public);
			context.Response.Cache.SetETag(ownETag);

			if (ownETag == clientETag) {
				context.Response.StatusCode = 304;
				context.Response.Flush();
				context.Response.End();
			}
			else {
                HttpCompression.EnableHttpCompression(context);
				context.Response.Write(doc.InnerXml);
				context.Response.ContentType = "application/rss+xml";
				context.Response.End();
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
