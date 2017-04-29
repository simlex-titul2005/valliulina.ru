using SX.WebCore.MvcApplication;
using System;
using System.Collections.Generic;
using System.Configuration;
using vru.Infrastructure;
using vru.Models;

namespace vru
{
    public class MvcApplication : SxMvcApplication
    {
        private static Dictionary<string, string> _settings;
        public static Dictionary<string, string> Settings { get { return _settings; } }

        protected override void Application_Start(object sender, EventArgs e)
        {
            fillSettings();

            var args = new SxApplicationEventArgs(
                    defaultSiteName: "valliulina.ru",
                    getDbContextInstance: () => { return new Infrastructure.DbContext(); },
                    getModelCoreTypeName: getModelCoreTypeNameFunc,
                    customModelCoreTypes:new Dictionary<string, byte>(),
                    repoProvider: new RepoProvider()
                );

            args.WebApiConfigRegister = WebApiConfig.Register;
            args.MapperConfigurationExpression = cfg => { AutoMapperConfig.Register(cfg); };

            //routes
            args.DefaultControllerNamespaces = new string[] { "vru.Controllers" };
            args.PreRouteAction = RouteConfig.PreRouteAction;
            args.PostRouteAction = RouteConfig.PostRouteAction;

            base.Application_Start(sender, args);
        }

        private static string getModelCoreTypeNameFunc(byte key)
        {
            switch(key)
            {
                default: return null;
                case 1: return nameof(Article);
            }
        }

        private static void fillSettings()
        {
            _settings = new Dictionary<string, string>();
            _settings.Add("phone1", ConfigurationManager.AppSettings["phone1"]);
            _settings.Add("phone2", ConfigurationManager.AppSettings["phone2"]);
            _settings.Add("defDesc", ConfigurationManager.AppSettings["defDesc"]);
            _settings.Add("defH1", ConfigurationManager.AppSettings["defH1"]);
            _settings.Add("defKeywords", ConfigurationManager.AppSettings["defKeywords"]);
        }
    }
}
