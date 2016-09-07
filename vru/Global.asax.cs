using SX.WebCore.MvcApplication;
using System;
using System.Collections.Generic;
using System.Configuration;
using vru.Models;
using vru.ViewModels;

namespace vru
{
    public class MvcApplication : SxApplication<Infrastructure.DbContext>
    {
        private static Dictionary<string, string> _settings;
        public static Dictionary<string, string> Settings { get { return _settings; } }

        protected override void Application_Start(object sender, EventArgs e)
        {
            fillSettings();

            var args = new SxApplicationEventArgs();
            args.WebApiConfigRegister = WebApiConfig.Register;
            args.RegisterRoutes = RouteConfig.RegisterRoutes;
            args.LoggingRequest = true;
            args.MapperConfigurationExpression = (cfg) =>
            {
                //article
                cfg.CreateMap<Article, VMArticle>();
                cfg.CreateMap<VMArticle, Article>();

                //education
                cfg.CreateMap<Education, VMEducation>();
                cfg.CreateMap<VMEducation, Education>();

                //question
                cfg.CreateMap<Question, VMQuestion>();
                cfg.CreateMap<VMQuestion, Question>();

                //service
                cfg.CreateMap<Service, VMService>();
                cfg.CreateMap<VMService, Service>();

                //situation
                cfg.CreateMap<Situation, VMSituation>();
                cfg.CreateMap<VMSituation, Situation>();
            };
            base.Application_Start(sender, args);
        }

        private static void fillSettings()
        {
            _settings = new Dictionary<string, string>();
            _settings.Add("phone1", ConfigurationManager.AppSettings["phone1"]);
            _settings.Add("phone2", ConfigurationManager.AppSettings["phone2"]);
            _settings.Add("defDesc", ConfigurationManager.AppSettings["defDesc"]);
            _settings.Add("defKeywords", ConfigurationManager.AppSettings["defKeywords"]);
        }
    }
}
