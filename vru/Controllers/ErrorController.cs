﻿using System.Web.Mvc;

namespace vru.Controllers
{
    public sealed class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}