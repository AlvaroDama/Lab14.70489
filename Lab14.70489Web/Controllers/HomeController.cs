using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Lab14._70489Web.Controllers
{
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            if (Session["sp"] == null)
                Session["sp"] = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            
            return View();
        }

        public ActionResult Diagnosticos()
        {
            Configuration currentConfig = WebConfigurationManager.OpenWebConfiguration("~");
            TraceSection traceSection = (TraceSection) currentConfig.GetSection("system.web/trace");
            ViewBag.TracingStatus = traceSection.Enabled;

            return View();
        }

        public ActionResult ToggleTracing(bool estado)
        {
            Configuration currentConfig = WebConfigurationManager.OpenWebConfiguration("~");
            TraceSection traceSection = (TraceSection)currentConfig.GetSection("system.web/trace");
            traceSection.Enabled = estado;
            currentConfig.Save();

            ViewBag.TracingStatus = estado;
            return View("Diagnosticos");
        }
        
    }
}
