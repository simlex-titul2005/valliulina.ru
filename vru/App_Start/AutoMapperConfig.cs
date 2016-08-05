using AutoMapper;
using SX.WebCore;
using SX.WebCore.ViewModels;
using vru.Models;
using vru.ViewModels;

namespace vru
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfigurationInstance
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    //article
                    cfg.CreateMap<Article, VMArticle>();
                    cfg.CreateMap<VMArticle, Article>();

                    //education
                    cfg.CreateMap<Education, VMEducation>();
                    cfg.CreateMap<VMEducation, Education>();

                    //picture
                    cfg.CreateMap<SxPicture, SxVMPicture>();
                    cfg.CreateMap<SxVMPicture, SxPicture>();
                    cfg.CreateMap<SxPicture, SxVMEditPicture>();
                    cfg.CreateMap<SxVMEditPicture, SxPicture>();

                    //question
                    cfg.CreateMap<Question, VMQuestion>();
                    cfg.CreateMap<VMQuestion, Question>();

                    //301 redirect
                    cfg.CreateMap<Sx301Redirect, SxVM301Redirect>();
                    cfg.CreateMap<Sx301Redirect, SxVMEdit301Redirect>();
                    cfg.CreateMap<SxVMEdit301Redirect, Sx301Redirect>();

                    //seo keyword
                    cfg.CreateMap<SxSeoKeyword, SxVMSeoKeyword>();
                    cfg.CreateMap<SxSeoKeyword, SxVMEditSeoKeyword>();
                    cfg.CreateMap<SxVMEditSeoKeyword, SxSeoKeyword>();

                    //seo tags
                    cfg.CreateMap<SxSeoTags, SxVMSeoTags>();
                    cfg.CreateMap<SxVMSeoTags, SxSeoTags>();
                    cfg.CreateMap<SxSeoTags, SxVMEditSeoTags>();
                    cfg.CreateMap<SxVMEditSeoTags, SxSeoTags>();

                    //service
                    cfg.CreateMap<Service, VMService>();
                    cfg.CreateMap<VMService, Service>();

                    //situation
                    cfg.CreateMap<Situation, VMSituation>();
                    cfg.CreateMap<VMSituation, Situation>();

                    //situation
                    cfg.CreateMap<SxAppUser, SxVMAppUser>();
                });
            }
        }
    }
}