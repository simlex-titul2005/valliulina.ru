using Microsoft.AspNet.Identity;
using SX.WebCore.MvcApplication;
using SX.WebCore.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace vru
{
    public class MvcApplication : SxApplication<Infrastructure.DbContext>
    {
        private static Dictionary<string, string> _settings;
        public static Dictionary<string, string> Settings { get { return _settings; } }

        private static DateTime _lastStartDate;

        protected override void Application_Start(object sender, EventArgs e)
        {
            fillSettings();

            var args = new SxApplicationEventArgs();
            args.WebApiConfigRegister = WebApiConfig.Register;
            args.RegisterRoutes = RouteConfig.RegisterRoutes;
            args.MapperConfiguration = AutoMapperConfig.MapperConfigurationInstance;
            args.LogDirectory = null;
            args.LoggingRequest = true;
            base.Application_Start(sender, args);

            _lastStartDate = DateTime.Now;
            Database.SetInitializer<Infrastructure.DbContext>(null);
            var siteDomainItem = new SxRepoSiteSetting<Infrastructure.DbContext>().GetByKey("siteDomain");
            SiteDomain = siteDomainItem?.Value;
        }

        private static void fillSettings()
        {
            _settings = new Dictionary<string, string>();
            _settings.Add("phone1", ConfigurationManager.AppSettings["phone1"]);
            _settings.Add("phone2", ConfigurationManager.AppSettings["phone2"]);
        }

        public static DateTime LastStartDate
        {
            get
            {
                return _lastStartDate;
            }
        }

        protected override void Session_Start()
        {
            var sessionId = Session.SessionID;
            if (!UsersOnSite.ContainsKey(sessionId))
                UsersOnSite.Add(sessionId, null);

            if (User.Identity.IsAuthenticated)
                UsersOnSite[sessionId] = User.Identity.GetUserName();
        }

        protected override void Session_End()
        {
            var sessionId = Session.SessionID;
            if (UsersOnSite.ContainsKey(sessionId))
                UsersOnSite.Remove(sessionId);
        }
    }
}
