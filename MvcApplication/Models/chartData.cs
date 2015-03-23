using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class chartData
    {
        public List<string> dates = new List<string>();
        public List<series> series = new List<series>();
        public List<yAxis> yAxis = new List<yAxis>();
        public string chartTitle="";
        
        public string seriesName = "";
        public DateTime startDate;
        public DateTime endDate;
        public Boolean day = false;
    }
}