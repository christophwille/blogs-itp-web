using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace blogs.dotnetgerman.com {
    public class HttpCompression {
        public static CompressingFilter GetFilterForScheme(string[] schemes, Stream output)
        {
            bool foundDeflate = false;
            bool foundGZip = false;
            bool foundStar = false;

            bool isAcceptableDeflate;
            bool isAcceptableGZip;
            bool isAcceptableStar;

            for (int i = 0; i < schemes.Length; i++) {
                string acceptEncodingValue = schemes[i].Trim().ToLower();

                if (acceptEncodingValue.StartsWith("deflate")) {
                    foundDeflate = true;


                }

                else if (acceptEncodingValue.StartsWith("gzip") || acceptEncodingValue.StartsWith("x-gzip")) {
                    foundGZip = true;


                }

                else if (acceptEncodingValue.StartsWith("*")) {
                    foundStar = true;


                }
            }

            isAcceptableStar = foundStar;
            isAcceptableDeflate = (foundDeflate) || (!foundDeflate && isAcceptableStar);
            isAcceptableGZip = (foundGZip) || (!foundGZip && isAcceptableStar);



            // do they support any of our compression methods?
            if (!(isAcceptableDeflate || isAcceptableGZip || isAcceptableStar)) {
                return null;
            }

            if (isAcceptableDeflate || isAcceptableStar)
                return new DeflateFilter(output, CompressionLevels.Default);
            if (isAcceptableGZip)
                return new GZipFilter(output);

            // return null.  we couldn't find a filter.
            return null;
        }



        public static void EnableHttpCompression(HttpContext Context)
        {
            if (!string.IsNullOrEmpty(Context.Request.Headers.Get("Accept-Encoding"))) {
                string acceptEncoding = Context.Request.Headers["Accept-Encoding"];
                string[] types =
                    acceptEncoding.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (types.Length > 0) {
                    CompressingFilter filter =
                        HttpCompression.GetFilterForScheme(types, Context.Response.Filter);
                    if (null != filter) {
                        Context.Response.Filter = filter;
                    }
                }
            }
        }




    }

    public enum CompressionLevels {
        /// <summary>Use the default compression level</summary>
        Default = -1,
        /// <summary>The highest level of compression.  Also the slowest.</summary>
        Highest = 9,
        /// <summary>A higher level of compression.</summary>
        Higher = 8,
        /// <summary>A high level of compression.</summary>
        High = 7,
        /// <summary>More compression.</summary>
        More = 6,
        /// <summary>Normal compression.</summary>
        Normal = 5,
        /// <summary>Less than normal compression.</summary>
        Less = 4,
        /// <summary>A low level of compression.</summary>
        Low = 3,
        /// <summary>A lower level of compression.</summary>
        Lower = 2,
        /// <summary>The lowest level of compression that still performs compression.</summary>
        Lowest = 1,
        /// <summary>No compression.  Use this is you are quite silly.</summary>
        None = 0
    }


}
