using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public Models.chartData values = new Models.chartData
        {
            startDate = DateTime.Parse("01/01/15"),
            endDate = DateTime.Parse("31/01/15"),
            seriesName = "Index"
        };

        public ActionResult Index()
        {
            return View(values);
        }
        
        [HttpPost]
        public ActionResult Index(string startDate,string endDate)
        {
            values.startDate = DateTime.Parse(startDate);
            values.endDate = DateTime.Parse(endDate);
            
            return View("Index",values);
        }
        public ActionResult LWA(string startDate,string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            marketDataDataContext data = new marketDataDataContext();
            var query = from LWA in data.LWAs where LWA.Date >= values.startDate where LWA.Date <= values.endDate select LWA;
            
            values.chartTitle = "Load Weighted Average";
            values.seriesName = "LWA";
            
            values.series.Add(new Models.series("LWA"));

            values.series.Add(new Models.series("Seven Day LWA"));
            values.series[1].type = "spline";

            values.yAxis.Add(new Models.yAxis("LWA (€/MWH)"));
            foreach (var s in query)
            {
                values.dates.Add('"'+s.Date.ToShortDateString()+'"');
                values.series[0].data.Add(s.Lwa1);
                values.series[1].data.Add(s.SevenDayLWA);
            }
            return View("Chart", values);
        }
        public ActionResult Max_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.MaxSMPs where smp.Date >= values.startDate where smp.Date <= values.endDate select smp;
            
            values.chartTitle = "Maximum System Marginal Price (SMP)";
            values.seriesName = "Max_SMP";

            values.yAxis.Add(new Models.yAxis("System Marginal Price - SMP (€/MWh)"));

            values.series.Add(new Models.series("Max SMP"));

            values.series.Add(new Models.series("Seven Day Max SMP"));
            values.series[1].type = "spline";


            foreach (var s in query)
            {
                values.dates.Add('"' + s.Date.ToShortDateString() + '"');
                values.series[0].data.Add(s.MaxSMP1);
                values.series[1].data.Add(s.SevenDayMaxSMP);
            }
            return View("Chart", values);
        }
        public ActionResult Min_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.MinSMPs where smp.Date >= values.startDate where smp.Date <= values.endDate select smp;
            
            values.chartTitle = "Minimum System Marginal Price (SMP)";
            values.seriesName = "Min_SMP";

            values.yAxis.Add(new Models.yAxis("System Marginal Price - SMP (€/MWh)"));

            values.series.Add(new Models.series("Min SMP"));

            values.series.Add(new Models.series("Seven Day Min SMP"));
            values.series[1].type = "spline";

            foreach (var s in query)
            {
                values.dates.Add('"' + s.Date.ToShortDateString() + '"');
                values.series[0].data.Add(s.MinSMP1);
                values.series[1].data.Add(s.SevenDayMinSMP);
            }
            return View("Chart", values);
        }
        public ActionResult Shadow_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(startDate).AddDays(1);
            }
            values.endDate = values.startDate.AddDays(1);
            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.Shadow_SMPs where smp.Date >= values.startDate where smp.Date < values.endDate select smp;
            
            values.chartTitle = "Shadow Price and System Marginal Price (SMP)";
            values.seriesName = "Shadow_SMP";
            values.day = true;

            values.yAxis.Add(new Models.yAxis("SMP / Shadow Price (€/MWh)"));

            values.series.Add(new Models.series("SMP"));
            values.series[0].type = "spline";

            values.series.Add(new Models.series("Shadow Price"));
            values.series[1].type = "spline";
        
            foreach (var s in query)
            {
                values.dates.Add('"' + s.Date.ToShortTimeString() + '"');
                values.series[0].data.Add(s.SMP);
                values.series[1].data.Add(s.ShadowPrice);
            }
            return View("Chart", values);
        }
        public ActionResult Load_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(startDate).AddDays(1);
            }
            values.endDate = values.startDate.AddDays(1);

            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.SMP_Loads where smp.Date >= values.startDate where smp.Date < values.endDate select smp;
            
            values.chartTitle = "SMP vs Load";
            values.seriesName = "Load_SMP";
            values.day = true;

            values.yAxis.Add(new Models.yAxis("System Load (MW)"));

            values.yAxis.Add(new Models.yAxis("SMP (€/MWh)"));
            values.yAxis[1].opposite = true;

            values.series.Add(new Models.series("System Load"));

            values.series.Add(new Models.series("SMP"));
            values.series[1].type = "spline";
            values.series[1].yAxis = 1;

            foreach (var s in query)
            {
                values.dates.Add("'" + s.Date.ToShortTimeString() + "'");
                values.series[1].data.Add(s.SMP);
                values.series[0].data.Add(s.SystemLoad);
            }   
            return View("Chart", values);
        }
        public ActionResult Chart(Models.chartData values)
        {
            return View(values);
        }

    }
}
