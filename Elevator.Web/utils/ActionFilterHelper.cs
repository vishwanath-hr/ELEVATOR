using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Elevator.Web.utils
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                var errors = (from key in modelState.Keys select modelState[key] into state where state.Errors.Any() select state.Errors.First().ErrorMessage).ToList();
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(errors)) };
            }
        }
    }
}