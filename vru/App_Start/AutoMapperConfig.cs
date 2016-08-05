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

                    //service
                    cfg.CreateMap<Service, VMService>();
                    cfg.CreateMap<VMService, Service>();
                });
            }
        }
    }
}