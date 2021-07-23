using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Models
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper,string src,string alt,string width, string height)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttribute("src", VirtualPathUtility.ToAbsolute(src));
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}