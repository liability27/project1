using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class chartData
    {
        public List<string> dates = new List<string>();
        public List<string> series1 = new List<string>();
        public List<string> series2 = new List<string>();
        public string chartTitle="";
        public string axisTitle = "";
        public string seriesName = "";
        public DateTime startDate;
        public DateTime endDate;
        public Boolean day = false;
        public string addedScript = "";
    }
}