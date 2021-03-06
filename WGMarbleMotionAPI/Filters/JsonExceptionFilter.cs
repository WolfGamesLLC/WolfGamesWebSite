﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WolfGamesWebSite.DAL.Models;

namespace WGMarbleMotionAPI.Filters
{
    /// <summary>
    /// Serialize error messages to Json before exporting to the
    /// client
    /// </summary>
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Allow the injection of the hosting environment
        /// </summary>
        /// <param name="env">The hosting environment</param>
        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Convert the context's exception data to an ApiError view
        /// model for display in a Json format
        /// </summary>
        /// <param name="context">The source <see cref="ExceptionContext"/></param>
        public void OnException(ExceptionContext context)
        {
            var err = new ApiError();

            if (_env.IsDevelopment())
            {
                err.Message = context.Exception.Message;
                err.Detail = context.Exception.StackTrace;
            }
            else
            {
                err.Message = "A server error occurred.";
                err.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(err)
            {
                StatusCode = (int) HttpStatusCode.InternalServerError
            };
        }
    }
}
