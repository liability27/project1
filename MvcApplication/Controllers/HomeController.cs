using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {

        public Models.chartData values = new Models.chartData
        {
            startDate = DateTime.Parse("01/01/15"),
            endDate = DateTime.Parse("31/01/15"),
            seriesName = "Index"
        };

        public ActionResult Index()
        {
            marketDataDataContext con = new marketDataDataContext();

            List<IQueryable> query = new List<IQueryable>();
            values.dates.Add((from dates in con.LWAs select dates.Date).Max().ToShortDateString());
            values.dates.Add((from dates in con.MaxSMPs select dates.Date).Max().ToShortDateString());
            values.dates.Add((from dates in con.MinSMPs select dates.Date).Max().ToShortDateString());
            values.dates.Add((from dates in con.Shadow_SMPs select dates.Date).Max().AddDays(-1).ToShortDateString());
            values.dates.Add((from dates in con.SMP_Loads select dates.Date).Max().AddDays(-1).ToShortDateString());

            return View(values);
        }
        public ActionResult LWA(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            Scripts.chartValues x = new Scripts.chartValues();
            return View("Chart", x.LWA(values.startDate,values.endDate));
        }
        public ActionResult Max_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            Scripts.chartValues x = new Scripts.chartValues();
            return View("Chart", x.Max_SMP(values.startDate, values.endDate));
        }
        public ActionResult Min_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(endDate);
            }
            Scripts.chartValues x = new Scripts.chartValues();
            return View("Chart", x.Min_SMP(values.startDate, values.endDate));
        }
        public ActionResult Shadow_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(startDate).AddDays(1);
            }
            Scripts.chartValues x = new Scripts.chartValues();
            return View("Chart", x.Shadow_SMP(values.startDate));
        }
        public ActionResult Load_SMP(string startDate, string endDate)
        {
            if (Request.HttpMethod == "POST")
            {
                values.startDate = DateTime.Parse(startDate);
                values.endDate = DateTime.Parse(startDate).AddDays(1);
            }
            Scripts.chartValues x = new Scripts.chartValues();
            return View("Chart", x.Load_SMP(values.startDate));
        }
        public ActionResult updateLWA()
        {
            Program.updateLWA();
            return RedirectToAction("Index");
        }
        public ActionResult updateMaxSMP()
        {
            Program.updateMaxSMP();
            return RedirectToAction("Index");
        }
        public ActionResult updateMinSMP()
        {
            Program.updateMinSMP();
            return RedirectToAction("Index");
        }
        public ActionResult updateShadow_Smp()
        {
            Program.updateShadow_Smp();
            return RedirectToAction("Index");
        }
        public ActionResult updateSmp_Load()
        {
            Program.updateSmp_Load();
            return RedirectToAction("Index");
        }
    }
}
