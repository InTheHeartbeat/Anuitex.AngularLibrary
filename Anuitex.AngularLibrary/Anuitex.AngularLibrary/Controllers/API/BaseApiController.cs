using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Anuitex.AngularLibrary.Data;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class BaseApiController : ApiController
    {
        protected Account CurrentUser { get; private set; }
        protected Visitor CurrentVisitor { get; private set; }

        protected LibraryDataContext DataContext { get; private set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            DataContext = new LibraryDataContext();

            InitializeCurrentUser(controllerContext.Request);
            InitializeCurrentVisitor(controllerContext.Request);
        }

        private void InitializeCurrentUser(HttpRequestMessage requestContext)
        {
            string at = requestContext.Headers.GetCookies("AToken")?.FirstOrDefault()?["AToken"]?.Value;

            string adr = "";
            if (requestContext.Properties.ContainsKey("MS_HttpContext"))
            {
                adr = ((HttpContextWrapper)requestContext.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            if (!string.IsNullOrWhiteSpace(at) && !string.IsNullOrWhiteSpace(adr))
            {
                Guid token = Guid.Parse(at);
                CurrentUser = DataContext.AccountAccessRecords.FirstOrDefault(t => t.Token == token && t.Source == adr)?.Account;
            }
        }

        private void InitializeCurrentVisitor(HttpRequestMessage requestContext)
        {
            string vt = requestContext.Headers.GetCookies("VToken")?.FirstOrDefault()?["VToken"]?.Value; ;
            if (vt != null)
            {
                Guid guid = Guid.Parse(vt);
                CurrentVisitor = DataContext.Visitors.FirstOrDefault(v => v.Token == guid);
            }
        }
    }
}
