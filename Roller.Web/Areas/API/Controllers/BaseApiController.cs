//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Roller.Repository.Interface;

//namespace Roller.Web.Areas.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public abstract class BaseApiController : Controller
//    {
//        private readonly IMapper _mapper;
//        private readonly IHttpContextAccessor _HttpContextAccessor;

//        private readonly IServiceProvider _serviceProvider;

//        //protected IHttpContextAccessor HttpContextAccessor => _serviceProvider ?? (_serviceProvider = HttpContext.RequestServices.GetService<IHttpContextAccessor>());
//        //protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
//        //public BaseApiController(IServiceProvider serviceProvider, IMapper mapper,IHttpContextAccessor HttpContextAccessor)
//        //{
//        //    _mapper = mapper;
//        //    _serviceProvider = serviceProvider;
//        //    _HttpContextAccessor = HttpContextAccessor;

//        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//        //    //protected IServiceProvider ServiceProvider = .GetService<IDependencyRegister>();
//        //}

//        public void  Index()
//        {
//            // IHttpContextAccessor contextAccessor = HttpContext.User.Identity.Name;
//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
//            //  IServiceProvider ServiceProvider = _serviceProvider.GetService<>();

//        }
//    }
//}


  


