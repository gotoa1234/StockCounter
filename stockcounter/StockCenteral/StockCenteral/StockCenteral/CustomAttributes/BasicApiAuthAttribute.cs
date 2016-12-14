using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace StockCenteral.CustomAttributes
{
    public class BasicApiAuthAttribute : AuthorizationFilterAttribute
    {
        //[Dependency]
        //internal IAuthorization _Authorization { get; set; }

        //public BasicApiAuthAttribute()
        //    : this(DependencyResolver.Current.GetService<IAuthorization>())
        //{
        //}
        //public BasicApiAuthAttribute(IAuthorization Authorization)
        //{
        //    _Authorization = Authorization;
        //}

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    if (!_Authorization.IsAuthorizated(new HttpContextWrapper(HttpContext.Current)))
        //        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);


        //}
    }
}