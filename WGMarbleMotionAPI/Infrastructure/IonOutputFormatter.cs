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
        private readonly JsonOutputFormatter _jsonOutputFormatter;

        /// <summary>
        /// Default constructor for the IonOutputFormatter which is 
        /// used to convert the output data to Ion+Json
        /// </summary>
        /// <param name="jsonOutputFormatter">An instance of a JsonOutputFormatter</param>
        public IonOutputFormatter(JsonOutputFormatter jsonOutputFormatter)
        {
            if (jsonOutputFormatter == null) throw new ArgumentNullException(nameof(jsonOutputFormatter));
            _jsonOutputFormatter = jsonOutputFormatter;
        }
    }
}
