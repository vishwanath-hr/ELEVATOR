﻿using System.Web;
using System.Web.Mvc;
using Elevator.Web.utils;

namespace Elevator.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}