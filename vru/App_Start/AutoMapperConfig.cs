using AutoMapper;
using vru.Models;
using vru.ViewModels;

namespace vru
{
    public class AutoMapperConfig
    {
        public static void Register(IMapperConfigurationExpression cfg)
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

            //service
            cfg.CreateMap<Situation, VMSituation>();
            cfg.CreateMap<VMSituation, Situation>();
        }
    }
}