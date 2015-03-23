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
        //Main method to be run to initialise a new database
        public static void Main(string[] args)
        {
            updateLWA();
            updateMaxSMP();
            updateMinSMP();
            updateShadow_Smp();
            updateSmp_Load();
        }
        //this method checks the LWA table for the last entry and updates data
        //up until yesterday
        public static void updateLWA()
        {
            //Declare Datetime objects
            DateTime startDate, endDate;
            
            //initialise a new object to access database
            marketDataDataContext dtbase = new marketDataDataContext();
            //query LWA table for dates entered
            var lastDate = from u in dtbase.LWAs
                           select u.Date;
            //int to store number of dates to update
            int dataPoints;

            //calculate startDate, endDate and Datapoints
            getRange(out startDate, out endDate, out dataPoints, lastDate.Max().AddDays(1));
            
            //variable to store LWA info
            LWA info;
            
            //loop for all days to be entered
            for (int i = 0; i < dataPoints; i++)
            {
                //call getLWA method and store returned values
                info = getLWA(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                //mark values to be inserted
                dtbase.LWAs.InsertOnSubmit(info);

                //if the value does not enter in database continue to next day
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
        //method to calculate startDate, endDate and dataPoints
        private static void getRange(out DateTime startDate, out DateTime endDate, out int dataPoints, DateTime lastDate)
        {
            try
            {
                //store last date value;
                startDate = lastDate;
            }
            catch
            {
                //used for initialising the database
                //if last date is a null default to 1/1/2010
                startDate = DateTime.Parse("1/1/2010");
            }
            //store end date as day yesterday
            endDate = DateTime.Now.AddDays(-1);

            //calculate number of days to update
            dataPoints = (endDate - startDate).Days + 1;

            //ensure that datapoints is 0 if database is up to date
            if (endDate < startDate)
            {
                dataPoints = 0;
            }
        }
        //returns an array of LWA data on specified date from semo
        public static LWA getLWA(string date)
        {
            //declare and initialise strings
            string htmlCode = "", data = "";
            
            //get the webpage from semo for the LWA table on date given in method argument
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Load%20Weighted%20Average%20SMP&prm_GetFromDate=" + date + "&prm_GetToDate=" + date + "&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            //boolean to determine if data should be stored
            bool begin = false;
            
            //loop through all elements in the html page
            foreach (string s in htmlCode.Split('>'))
            {
                //search for "Trade Date" to begin storing values
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                //search for "Run Date" to stop storing values
                if (s.StartsWith(@"Run Date"))
                {
                    begin = false;
                }
                //store the string of data values plus the table head
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            //store the values in the string as a LWA object and return
            LWA info = new LWA(data.Split(',')[5], data.Split(',')[6], data.Split(',')[7], data.Split(',')[8], data.Split(',')[9]);

            return info;
        }
        //this method checks the Max_SMP table for the last entry and updates data
        //up until yesterday
        public static void updateMaxSMP()
        {
            //Declare Datetime objects
            DateTime startDate, endDate;

            //initialise a new object to access database
            marketDataDataContext dtbase = new marketDataDataContext();
            //query MaxSMP table for dates entered
            var lastDate = from u in dtbase.MaxSMPs
                           select u.Date;
            //int to store number of dates to update
            int dataPoints;

            //calculate startDate, endDate and Datapoints
            getRange(out startDate, out endDate, out dataPoints, lastDate.Max().AddDays(1));

            //variable to store MaxSMP info
            MaxSMP info;

            //loop for days to be entered
            for (int i = 0; i < dataPoints; i++)
            {
                //call getMaxSMP and store returned values
                info = getMaxSMP(startDate.AddDays(i).ToString("dd-MMM-yyyy"));
                
                //mark info for submitting to database
                dtbase.MaxSMPs.InsertOnSubmit(info);

                //if value does not enter continue to next day
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
        //returns an array of MaxSMP data on specified date from semo
        public static MaxSMP getMaxSMP(string date)
        {
            //declare and initialise strings
            string htmlCode = "", data = "";

            //get the webpage from semo for the LWA table on date given in method argument
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Maximum%20SMP&prm_GetFromDate=" + date + @"&prm_GetToDate=" + date + @"&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            //boolean to determine if data should be stored
            bool begin = false;

            //loop through all elements in the html page
            foreach (string s in htmlCode.Split('>'))
            {
                //search for "Trade Date" to begin storing values
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                //search for "Run Date" to stop storing values
                if (s.StartsWith(@"Run Date"))
                {
                    begin = false;
                }
                //store the string of data values plus the table head
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            //store the values in the string as a MaxSMP object and return
            MaxSMP info = new MaxSMP(data.Split(',')[5], data.Split(',')[6], data.Split(',')[7], data.Split(',')[8], data.Split(',')[9]);

            return info;
        }
        //this method checks the Min_SMP table for the last entry and updates data
        //up until yesterday
        public static void updateMinSMP()
        {
            //Declare Datetime objects
            DateTime startDate, endDate;

            //initialise a new object to access database
            marketDataDataContext dtbase = new marketDataDataContext();
            //query MinSMP table for dates entered
            var lastDate = from u in dtbase.MinSMPs
                           select u.Date;
            //int to store number of dates to update
            int dataPoints;

            //calculate startDate, endDate and Datapoints
            getRange(out startDate, out endDate, out dataPoints, lastDate.Max().AddDays(1));

            //variable to store MinSMP info
            MinSMP info;

            //loop for days to be entered
            for (int i = 0; i < dataPoints; i++)
            {
                //call getMinSMP and store returned values
                info = getMinSMP(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                //mark info for submitting to database
                dtbase.MinSMPs.InsertOnSubmit(info);

                //if value does not enter continue to next day
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
        //returns an object of MinSMP data on specified date from semo
        public static MinSMP getMinSMP(string date)
        {
            //declare and initialise strings
            string htmlCode = "", data = "";

            //get the webpage from semo for the LWA table on date given in method argument
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Minimum%20SMP&prm_GetFromDate=" + date + @"&prm_GetToDate=" + date + @"&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            //boolean to determine if data should be stored
            bool begin = false;

            //loop through all elements in the html page
            foreach (string s in htmlCode.Split('>'))
            {
                //search for "Trade Date" to begin storing values
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                //search for "Run Date" to stop storing values
                if (s.StartsWith(@"Run Date"))
                {
                    begin = false;
                }
                //store the string of data values plus the table head
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            //store the values in the string as a MaxSMP object and return
            MinSMP info = new MinSMP(data.Split(',')[5], data.Split(',')[6], data.Split(',')[7], data.Split(',')[8], data.Split(',')[9]);

            return info;
        }
        //this method checks the Shadow_SMP table for the last entry and updates data
        //up until yesterday
        public static void updateShadow_Smp()
        {
            //Declare Datetime objects
            DateTime startDate, endDate;

            //initialise a new object to access database
            marketDataDataContext dtbase = new marketDataDataContext();
            //query MinSMP table for dates entered
            var lastDate = from u in dtbase.Shadow_SMPs
                           select u.Date;
            //int to store number of dates to update
            int dataPoints;

            //calculate startDate, endDate and Datapoints
            getRange(out startDate, out endDate, out dataPoints, lastDate.Max());//don't add a day to lastDate

            //variable to store MinSMP info
            List<Shadow_SMP> info;

            //loop for days to be entered
            for (int i = 0; i < dataPoints; i++)
            {
                //call getMinSMP and store returned values
                info = getShadow_SMP(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                //mark info for submitting to database
                dtbase.Shadow_SMPs.InsertAllOnSubmit(info);

                //if value does not enter try every half hour
                try
                {
                    dtbase.SubmitChanges();
                }
                catch
                {
                    //remove pending submits
                    dtbase.Shadow_SMPs.DeleteAllOnSubmit(info);
                    //try submit each half hour if fails move to next half hour
                    //will miss 4 half hours each year when times change
                    foreach (var x in info) { 
                        dtbase.Shadow_SMPs.InsertOnSubmit(x);
                        try { dtbase.SubmitChanges(); }
                        catch { dtbase.Shadow_SMPs.DeleteOnSubmit(x); }
                    }
                }
            }
        }
        //returns a list of Shadow_SMP objects with data on specified date from semo
        public static List<Shadow_SMP> getShadow_SMP(string date)
        {
            //declare and initialise strings
            string htmlCode = "", data = "";

            //get the webpage from semo for the LWA table on date given in method argument
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/Shadow%20Price%20and%20SMP&prm_GetDate=" + date + "&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            //boolean to determine if data should be stored
            bool begin = false;

            //loop through all elements in the html page
            foreach (string s in htmlCode.Split('>'))
            {
                //search for "Trade Date" to begin storing values
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                //search for "Run Date" to stop storing values
                if (s.StartsWith(@"Rows"))
                {
                    begin = false;
                }
                //store the string of data values plus the table head
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            //store the values in the string as a Shadow_SMP list
            List<Shadow_SMP> info = new List<Shadow_SMP>();
            String [] temp = data.Split(',');

            for (int i = 13; i < temp.Count() - 1; i += 7)
            {
                //add date annd time for a datetime object
                string temp_date = temp[i + 3] + " " + temp[i + 4];
                try
                {
                    info.Add(new Shadow_SMP(temp_date, temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                }
                //When clocks change semo add a string to the time
                catch
                {
                    try
                    {
                        //remove string from time
                        info.Add(new Shadow_SMP(temp_date.Substring(0, temp_date.Length - 3), temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                    }
                    catch
                    {
                        //go to next entry if add fails
                        continue;
                    }
                }
            }
            return info;
        }
        //this method checks the SMP_Load table for the last entry and updates data
        //up until yesterday
        public static void updateSmp_Load()
        {
            //Declare Datetime objects
            DateTime startDate, endDate;

            //initialise a new object to access database
            marketDataDataContext dtbase = new marketDataDataContext();
            //query MinSMP table for dates entered
            var lastDate = from u in dtbase.SMP_Loads
                           select u.Date;
            //int to store number of dates to update
            int dataPoints;

            //calculate startDate, endDate and Datapoints
            getRange(out startDate, out endDate, out dataPoints, lastDate.Max());//don't add a day to lastDate

            //variable to store MinSMP info
            List<SMP_Load> info;

            //loop for days to be entered
            for (int i = 0; i < dataPoints; i++)
            {
                //call getMinSMP and store returned values
                info = getSMP_Load(startDate.AddDays(i).ToString("dd-MMM-yyyy"));

                //mark info for submitting to database
                dtbase.SMP_Loads.InsertAllOnSubmit(info);

                //if value does not enter try every half hour
                try
                {
                    dtbase.SubmitChanges();
                }
                catch
                {
                    //remove pending submits
                    dtbase.SMP_Loads.DeleteAllOnSubmit(info);
                    //try submit each half hour if fails move to next half hour
                    //will miss 4 half hours each year when times change
                    foreach (var x in info)
                    {
                        dtbase.SMP_Loads.InsertOnSubmit(x);
                        try { dtbase.SubmitChanges(); }
                        catch { dtbase.SMP_Loads.DeleteOnSubmit(x); }
                    }
                }
            }
        }
        //returns a list of SMP_Load objects with data on specified date from semo
        public static List<SMP_Load> getSMP_Load(string date)
        {
            //declare and initialise strings
            string htmlCode = "", data = "";

            //get the webpage from semo for the LWA table on date given in method argument
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://semorep.sem-o.com/SemoWebSite/?qpReportServer=&qpReportURL=/SEMO%20Dynamic%20Reports/Dynamic%20Reporting%20-%20Predefined/All%20Predefined%20Reports/SMP%20v%20Load&prm_GetDate=" + date + "&prm_GetRunType=EA&prm_GetCurrency=EUR&prm_Chart_Table_Toggle=Table&qpWindowType=Popout&usr_Login=fbasemomember%3aniall_mcd%40hotmail.com&rpt_Toolbar=1&rpt_Print=1&rpt_Export=1&rpt_Zoom=1&rpt_ZoomPerc=100&rpt_Find=1&rpt_Navigate=1");
            }
            //boolean to determine if data should be stored
            bool begin = false;

            //loop through all elements in the html page
            foreach (string s in htmlCode.Split('>'))
            {
                //search for "Trade Date" to begin storing values
                if (s.StartsWith(@"Trade Date"))
                {
                    begin = true;
                }
                //search for "Run Date" to stop storing values
                if (s.StartsWith(@"Rows"))
                {
                    begin = false;
                }
                //store the string of data values plus the table head
                if ((!s.StartsWith("<")) && begin)
                {
                    data += s.Substring(0, s.Length - 5) + ",";
                }
            }
            //store the values in the string as a Shadow_SMP list
            List<SMP_Load> info = new List<SMP_Load>();
            String[] temp = data.Split(',');

            for (int i = 13; i < temp.Count() - 1; i += 7)
            {
                //add date annd time for a datetime object
                string temp_date = temp[i + 3] + " " + temp[i + 4];
                try
                {
                    info.Add(new SMP_Load(temp_date, temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                }
                //When clocks change semo add a string to the time
                catch
                {
                    try
                    {
                        //remove string from time
                        info.Add(new SMP_Load(temp_date.Substring(0, temp_date.Length - 3), temp[i + 1], temp[i + 2], temp[i + 5], temp[i + 6]));
                    }
                    catch
                    {
                        //go to next entry if add fails
                        continue;
                    }
                }
            }
            return info;
        }
    }
    //partial class contains a constructor for a new LWA object
    public partial class LWA
    {
        //stores arguments in class properties
        public LWA(string date, string RunType, string Currency, string LWA, string SevenDayLWA)
        {
            _Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.Lwa1 = decimal.Parse(LWA);
            this.SevenDayLWA = decimal.Parse(SevenDayLWA);
        }
    }
    //partial class contains a constructor for a new Max_SMP object
    public partial class MaxSMP
    {
        //stores arguments in class properties
        public MaxSMP(string date, string RunType, string Currency, string SMP, string SevenDaySMP)
        {
            this.Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.MaxSMP1 = decimal.Parse(SMP);
            this.SevenDayMaxSMP = decimal.Parse(SevenDaySMP);
        }
    }
    //partial class contains a constructor for a new Min_SMP object
    public partial class MinSMP
    {
        //stores arguments in class properties
        public MinSMP(string date, string RunType, string Currency, string SMP, string SevenDaySMP)
        {
            this.Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.MinSMP1 = decimal.Parse(SMP);
            this.SevenDayMinSMP = decimal.Parse(SevenDaySMP);
        }
    }
    //partial class contains a constructor for a new Shadow_SMP object
    public partial class Shadow_SMP
    {
        //stores arguments in class properties
        public Shadow_SMP(string date, string RunType, string Currency, string SMP, string ShadowPrice)
        {
            this.Date = DateTime.Parse(date);
            this.Run_Type = RunType;
            this.Currency = Currency;
            this.SMP = decimal.Parse(SMP);
            this.ShadowPrice = decimal.Parse(ShadowPrice);
        }
    }
    //partial class contains a constructor for a new SMP_Load object
    public partial class SMP_Load
    {
        //stores arguments in class properties
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