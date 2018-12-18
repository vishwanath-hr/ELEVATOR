using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elevator.Domain.Interfaces;

namespace Elevator.Web.Controllers
{
    public partial class HomeController : Controller
    {
        //
        public HomeController(){}
        // GET: /Home/
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Enquiry()
        {
            return View();
        }
    }
}
