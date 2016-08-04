using AutoMapper;
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
                    //picture
                    cfg.CreateMap<Picture, VMPicture>();

                    //service
                    cfg.CreateMap<Service, VMService>();
                    cfg.CreateMap<VMService, Service>();
                });
            }
        }
    }
}