using SX.WebCore.MvcControllers.Abstract;
using SX.WebCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using vru.Infrastructure;

namespace vru.Controllers
{
    [AllowAnonymous]
    public abstract class BaseController : SxBaseController
    {
        protected override Action<SxBaseController, HashSet<SxVMBreadcrumb>> FillBreadcrumbs
            => BreadcrumbsManager.WriteBreadcrumbs;
    }
}