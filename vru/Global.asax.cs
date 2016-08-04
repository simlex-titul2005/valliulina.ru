using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace vru
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Dictionary<string, string> _settings;
        public static Dictionary<string, string> Settings
        {
            get { return _settings; }
        }

        private static DateTime _lastStartDate;
        public static DateTime LastStartDate
        {
            get { return _lastStartDate; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            fillSettings();

            _lastStartDate = DateTime.Now;
        }

        private static void fillSettings()
        {
            _settings = new Dictionary<string, string>();
            _settings.Add("phone1", ConfigurationManager.AppSettings["phone1"]);
            _settings.Add("phone2", ConfigurationManager.AppSettings["phone2"]);
            _settings.Add("siteName", ConfigurationManager.AppSettings["siteName"]);
            _settings.Add("defH1", ConfigurationManager.AppSettings["defH1"]);
        }
    }
}
