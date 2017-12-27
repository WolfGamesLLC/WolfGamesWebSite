﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new ApiError())
            {
                StatusCode = 500
            };
        }
    }
}