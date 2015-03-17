using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class series
    {
        public string name;
        public List<decimal> data = new List<decimal>();
        public string type;
        public int yAxis;

        public series(string name)
        {
            this.name = name;
        }
    }
}