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

            //question
            cfg.CreateMap<Question, VMQuestion>();
            cfg.CreateMap<VMQuestion, Question>();

            //service
            cfg.CreateMap<Service, VMService>();
            cfg.CreateMap<VMService, Service>();
        }
    }
}