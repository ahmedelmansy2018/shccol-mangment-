﻿using System.Web;
using System.Web.Mvc;

namespace E_learning
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
         //   filters.Add(new AuthorizeAttribute());
        }
    }
}
