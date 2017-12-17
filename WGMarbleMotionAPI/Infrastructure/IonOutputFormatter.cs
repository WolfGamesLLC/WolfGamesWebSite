using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace WGMarbleMotionAPI.Infrastructure
{
    /// <summary>
    /// Utility class used to format the output following the ION
    /// specification
    /// </summary>
    public class IonOutputFormatter
    {
        private JsonOutputFormatter jsonOutputFormatter;

        public IonOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
        {
            this.jsonOutputFormatter = jsonOutputFormatter;
        }
    }
}
