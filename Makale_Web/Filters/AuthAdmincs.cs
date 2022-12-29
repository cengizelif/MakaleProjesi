using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Filters
{
    public class AuthAdmincs : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Kullanici kullanici =(Kullanici)filterContext.HttpContext.Session["login"];

            if ( kullanici != null && kullanici.Admin==false)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}