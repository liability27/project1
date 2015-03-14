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
            //values.viewSelect.Add("LWA",LWA());
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
            values.axisTitle = "LWA (€/MWH)";
            values.chartTitle = "Load Weighted Average";
            values.seriesName = "LWA";

            foreach (var s in query)
            {
                values.dates.Add("'" + s.Date.ToShortDateString() + "'");
                values.series1.Add(s.Lwa1.ToString());
                values.series2.Add(s.SevenDayLWA.ToString());
            }
            return View("Chart", values);
        }
        public ActionResult Max_SMP(string startDate,string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.MaxSMPs where smp.Date >= values.startDate where smp.Date <= values.endDate select smp;
            values.axisTitle = "System Marginal Price - SMP (€/MWh)";
            values.chartTitle = "Maximum System Marginal Price (SMP)";
            values.seriesName = "Max_SMP";

            foreach (var s in query)
            {
                values.dates.Add("'" + s.Date.ToShortDateString() + "'");
                values.series1.Add(s.MaxSMP1.ToString());
                values.series2.Add(s.SevenDayMaxSMP.ToString());
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
            values.axisTitle = "System Marginal Price - SMP (€/MWh)";
            values.chartTitle = "Minimum System Marginal Price (SMP)";
            values.seriesName = "Min_SMP";

            foreach (var s in query)
            {
                values.dates.Add("'" + s.Date.ToShortDateString() + "'");
                values.series1.Add(s.MinSMP1.ToString());
                values.series2.Add(s.SevenDayMinSMP.ToString());
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
            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.Shadow_SMPs where smp.Date >= values.startDate where smp.Date < values.endDate select smp;
            values.axisTitle = "SMP / Shadow Price (€/MWh)";
            values.chartTitle = "Shadow Price and System Marginal Price (SMP)";
            values.seriesName = "Shadow_SMP";
            values.day = true;
            foreach (var s in query)
            {
                values.dates.Add("'" + s.Date.ToShortTimeString() + "'");
                values.series1.Add(s.SMP.ToString());
                values.series2.Add(s.ShadowPrice.ToString());
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
            marketDataDataContext data = new marketDataDataContext();
            var query = from smp in data.Shadow_SMPs where smp.Date >= values.startDate where smp.Date < values.endDate select smp;
            values.axisTitle = "SMP / Shadow Price (€/MWh)";
            values.chartTitle = "Shadow Price and System Marginal Price (SMP)";
            values.seriesName = "Load_SMP";
            values.day = true;
            foreach (var s in query)
            {
                values.dates.Add("'" + s.Date.ToShortTimeString() + "'");
                values.series1.Add(s.SMP.ToString());
                values.series2.Add(s.ShadowPrice.ToString());
            }
            values.axisTitle += @"'}
        }, {
            opposite: true,
            title: {
                text: 'SMP (€/MWh)";
            return View("Chart", values);
        }
        public ActionResult Chart(Models.chartData values)
        {
            return View(values);
        }

    }
}
