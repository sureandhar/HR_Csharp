using System;
using Microsoft.AspNetCore.Mvc;
using Dhrms.DataAccess;
using System.Collections.Generic;

namespace Dhrms.WebService
{
    
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

    }
}
