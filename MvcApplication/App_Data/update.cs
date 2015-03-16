using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using MvcApplication;

namespace MvcApplication
{
    public class Program
    {
        private static marketDataDataContext dtbase = new marketDataDataContext();
        public static void Main(string[] args)
        {
            updateLWA();
            updateMaxSMP();
            updateMinSMP();
            updateShadow_Smp();
            updateSmp_Load();
        }
        public static void updateLWA()
        {
            DateTime startDate;
            var lastDate = from u in dtbase.LWAs
                           select u.Date;
            try
            {
                startDate = lastDate.Max().AddDays(1);
            }
            catch
            {
                startDate = DateTime.Parse("1/1/2010");
            }
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;
            if (endDate < startDate)
            {
                dataPoints = 0;
            }

            LWA info;
            for (int i = 0; i < dataPoints; i++)
            {
                info = getLWA(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                dtbase.LWAs.InsertOnSubmit(info);

                try
                {
                    dtbase.SubmitChanges();
                }
                catch
                {
                    continue;
                }
            }
        }
        //returns an array of LWA data on specified date from semo
        public static LWA getLWA(string date)
        {
            string htmlCode = "", data = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Load%20Weighted%20Average%20SMP&prm_GetFromDate=" + date + "&prm_GetToDate=" + date + "&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            bool begin = false;
            foreach (string s in htmlCode.Split('>'))
            {
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                if (s.StartsWith(@"Run Date"))
                {
                    begin = false;
                }
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            string[] temp = new string[5];
            LWA info = new LWA(data.Split(',')[5], data.Split(',')[6], data.Split(',')[7], data.Split(',')[8], data.Split(',')[9]);

            return info;
        }
        public static void checkLWA()
        {
            DateTime startDate = DateTime.Parse("1/1/2010");
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;

            var dateEntries = from date in dtbase.LWAs select date.Date;
            int count = 0;
            DateTime dateCounter;
            for (int i = 0; i < dataPoints; i++)
            {
                dateCounter = getLWA(startDate.AddDays(i).ToString("dd-MMM-yyyy")).Date;
                if (!dateEntries.Contains(dateCounter))
                {
                    count++;
                }
            }
        }
        public static void updateMaxSMP()
        {
            DateTime startDate;

            var lastDate = from u in dtbase.MaxSMPs
                           select u.Date;
            try
            {
                startDate = lastDate.Max().AddDays(1);
            }
            catch
            {
                startDate = DateTime.Parse("1/1/2010");
            }
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;
            if (endDate < startDate)
            {
                dataPoints = 0;
            }
            MaxSMP info;
            for (int i = 0; i < dataPoints; i++)
            {
                info = getMaxSMP(startDate.AddDays(i).ToString("dd-MMM-yyyy"));
                dtbase.MaxSMPs.InsertOnSubmit(info);

                try
                {
                    dtbase.SubmitChanges();
                }
                catch
                {
                    continue;
                }
            }
        }
        public static MaxSMP getMaxSMP(string date)
        {
            string htmlCode = "", data = "";
            using (WebClient client = new WebClient())
            {
                //htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Maximum%20SMP&prm_GetFromDate=01-Jan-2015&prm_GetToDate=31-Jan-2015&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Maximum%20SMP&prm_GetFromDate=" + date + @"&prm_GetToDate=" + date + @"&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            bool begin = false;
            foreach (string s in htmlCode.Split('>'))
            {
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                if (s.StartsWith(@"Run Date"))
                {
                    begin = false;
                }
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            string[] temp = new string[5];
            MaxSMP info = new MaxSMP(DateTime.ParseExact(data.Split(',')[5], "dd/MM/yyyy", CultureInfo.InvariantCulture), data.Split(',')[6], data.Split(',')[7], decimal.Parse(data.Split(',')[8]), decimal.Parse(data.Split(',')[9]));

            return info;
        }
        public static void updateMinSMP()
        {
            DateTime startDate;
            var lastDate = from u in dtbase.MinSMPs
                           select u.Date;
            try
            {
                startDate = lastDate.Max().AddDays(1);
            }
            catch
            {
                startDate = DateTime.Parse("1/1/2010");
            }
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;
            if (endDate < startDate)
            {
                dataPoints = 0;
            }


            MinSMP info;
            for (int i = 0; i < dataPoints; i++)
            {
                info = getMinSMP(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                //change to min
                dtbase.MinSMPs.InsertOnSubmit(info);

                try
                {
                    dtbase.SubmitChanges();
                }
                catch
                {
                    continue;
                }
            }
        }
        public static MinSMP getMinSMP(string date)
        {
            string htmlCode = "", data = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Minimum%20SMP&prm_GetFromDate=" + date + @"&prm_GetToDate=" + date + @"&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            bool begin = false;
            foreach (string s in htmlCode.Split('>'))
            {
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                if (s.StartsWith(@"Run Date"))
                {
                    begin = false;
                }
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            string[] temp = new string[5];
            MinSMP info = new MinSMP(DateTime.ParseExact(data.Split(',')[5], "dd/MM/yyyy", CultureInfo.InvariantCulture), data.Split(',')[6], data.Split(',')[7], decimal.Parse(data.Split(',')[8]), decimal.Parse(data.Split(',')[9]));

            return info;
        }
        public static void updateShadow_Smp()
        {
            DateTime startDate;
            var lastDate = from u in dtbase.Shadow_SMPs
                           select u.Date;
            try
            {
                startDate = lastDate.Max();
            }
            catch
            {
                startDate = DateTime.Parse("01/01/2010 6:00");
            }
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;
            if (endDate < startDate)
            {
                dataPoints = 0;
            }


            List<Shadow_SMP> info;
            for (int i = 0; i < dataPoints; i++)
            {
                info = getShadow_SMP(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                foreach (Shadow_SMP x in info)
                {
                    dtbase.Shadow_SMPs.InsertOnSubmit(x);
                }
                try
                {
                    dtbase.SubmitChanges();
                }
                catch
                {
                    foreach (var x in info)
                    {
                        dtbase.Shadow_SMPs.DeleteOnSubmit(x);
                    }
                    foreach (var x in info)
                    {
                        dtbase.Shadow_SMPs.InsertOnSubmit(x);
                        try { dtbase.SubmitChanges(); }
                        catch { dtbase.Shadow_SMPs.DeleteOnSubmit(x); }
                    }
                }
            }
        }
        public static List<Shadow_SMP> getShadow_SMP(string date)
        {
            string htmlCode = "", data = "";
            using (WebClient client = new WebClient())
            {
                //htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Load%20Weighted%20Average%20SMP&prm_GetFromDate=01-Jan-2014&prm_GetToDate=01-Jan-2014&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Shadow%20Price%20and%20SMP&prm_GetDate=" + date + "&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            bool begin = false;
            foreach (string s in htmlCode.Split('>'))
            {
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                if (s.StartsWith(@"Rows"))
                {
                    begin = false;
                }
                if ((!s.StartsWith("<")) && begin)
                {
                    //sr.WriteLine(s.Substring(0,s.Length-5));
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            List<Shadow_SMP> info = new List<Shadow_SMP>();
            String [] temp = data.Split(',');

            for (int i = 13; i < temp.Count() - 1; i += 7)
            {
                string temp_date = temp[i + 3] + " " + temp[i + 4];
                try
                {
                    info.Add(new Shadow_SMP(temp_date, temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                }
                catch
                {
                    try
                    {
                        info.Add(new Shadow_SMP(temp_date.Substring(0, temp_date.Length - 3), temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                    }
                    catch
                    {
                        throw new Exception();
                    }
                }
            }
            return info;
        }
        public static void checkShadowSMP()
        {
            DateTime startDate = DateTime.Parse("1/1/2010 6:00");
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;

            IQueryable<DateTime> dateEntries1 = from date in dtbase.Shadow_SMPs select date.Date;
            List<DateTime> dateEntries = dateEntries1.ToList();
            List<DateTime> allTimes = new List<DateTime>();
            
            while (startDate <= endDate)
            {
                if (!dateEntries.Contains(startDate))
                {
                    allTimes.Add(startDate);
                }
                startDate = startDate.AddMinutes(30);
            }
            foreach (var x in allTimes)
            {
                Console.WriteLine(x.ToString());
            }
        }
        public static void updateSmp_Load()
        {
            DateTime startDate;
            var lastDate = from u in dtbase.SMP_Loads
                           select u.Date;
            try
            {
                startDate = lastDate.Max();
            }
            catch
            {
                startDate = DateTime.Parse("01/01/2010 6:00");
            }
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;
            if (endDate < startDate)
            {
                dataPoints = 0;
            }


            List<SMP_Load> info;
            for (int i = 0; i < dataPoints; i++)
            {
                Console.WriteLine(startDate.AddDays(i).ToString("dd-MMM-yyyy"));
                info = getSMP_Load(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                Console.WriteLine("Getting: {0} out of {1}", i + 1, dataPoints);

                foreach (SMP_Load x in info)
                {
                    dtbase.SMP_Loads.InsertOnSubmit(x);
                }
                try
                {
                    dtbase.SubmitChanges();
                    Console.WriteLine("success");
                }
                catch
                {
                    foreach (var x in info)
                    {
                        dtbase.SMP_Loads.DeleteOnSubmit(x);
                    }
                    foreach (var x in info)
                    {
                        dtbase.SMP_Loads.InsertOnSubmit(x);
                        try { dtbase.SubmitChanges(); }
                        catch { dtbase.SMP_Loads.DeleteOnSubmit(x); }
                    }
                }
            }
        }
        public static List<SMP_Load> getSMP_Load(string date)
        {
            string htmlCode = "", data = "";
            using (WebClient client = new WebClient())
            {
                //htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Load%20Weighted%20Average%20SMP&prm_GetFromDate=01-Jan-2014&prm_GetToDate=01-Jan-2014&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/SMP%20v%20Load&prm_GetDate=" + date + "&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            bool begin = false;
            foreach (string s in htmlCode.Split('>'))
            {
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                if (s.StartsWith(@"Rows"))
                {
                    begin = false;
                }
                if ((!s.StartsWith("<")) && begin)
                {
                    //sr.WriteLine(s.Substring(0,s.Length-5));
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            List<SMP_Load> info = new List<SMP_Load>();
            String[] temp = data.Split(',');

            for (int i = 13; i < temp.Count() - 1; i += 7)
            {
                string temp_date = temp[i + 3] + " " + temp[i + 4];
                try
                {
                    info.Add(new SMP_Load(temp_date, temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                }
                catch
                {
                    try
                    {
                        info.Add(new SMP_Load(temp_date.Substring(0, temp_date.Length - 3), temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                    }
                    catch
                    {
                        throw new Exception();
                    }
                }
            }
            return info;
        }
        public static void checkSMPLoad()
        {
            DateTime startDate = DateTime.Parse("1/1/2010 6:00");
            DateTime endDate = DateTime.Now.AddDays(-1);
            int dataPoints = (endDate - startDate).Days + 1;

            IQueryable<DateTime> dateEntries1 = from date in dtbase.SMP_Loads select date.Date;
            List<DateTime> dateEntries = dateEntries1.ToList();
            List<DateTime> allTimes = new List<DateTime>();

            while (startDate <= endDate)
            {
                if (!dateEntries.Contains(startDate))
                {
                    allTimes.Add(startDate);
                }
                startDate = startDate.AddMinutes(30);
            }
            foreach (var x in allTimes)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
    public partial class LWA
    {
        public LWA(string date, string RunType, string Currency, string LWA, string SevenDayLWA)
        {
            _Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.Lwa1 = decimal.Parse(LWA);
            this.SevenDayLWA = decimal.Parse(SevenDayLWA);
        }
    }
    public partial class MaxSMP
    {
        public MaxSMP(DateTime date, string RunType, string Currency, decimal SMP, decimal SevenDaySMP)
        {
            this.Date = date;
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.MaxSMP1 = SMP;
            this.SevenDayMaxSMP = SevenDaySMP;
        }
    }
    public partial class MinSMP
    {
        public MinSMP(DateTime date, string RunType, string Currency, decimal SMP, decimal SevenDaySMP)
        {
            this.Date = date;
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.MinSMP1 = SMP;
            this.SevenDayMinSMP = SevenDaySMP;
        }
    }
    public partial class Shadow_SMP
    {
        public Shadow_SMP(string date, string RunType, string Currency, string SMP, string ShadowPrice)
        {
            this.Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.SMP = decimal.Parse(SMP);
            this.ShadowPrice = decimal.Parse(ShadowPrice);
        }
    }
    public partial class SMP_Load
    {
        public SMP_Load(string date, string RunType, string Currency, string SMP, string ShadowPrice)
        {
            this.Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.SMP = decimal.Parse(SMP);
            this.SystemLoad = decimal.Parse(ShadowPrice);
        }
    }

}