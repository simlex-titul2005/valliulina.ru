using AutoMapper;
using System.Web.Mvc;

namespace vru.Controllers
{
    public abstract class BaseController : Controller
    {
        private static IMapper _mapper;
        protected static IMapper Mapper
        {
            get { return _mapper; }
        }

        public BaseController()
        {
            if (_mapper == null)
                _mapper = AutoMapperConfig.MapperConfigurationInstance.CreateMapper();
        }
    }
}