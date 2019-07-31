using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace BookStoreAPI.ViewModel
{
    public class DashboardViewModel
    {

        
        
            public int Year { get; set; }
            public int Month { get; set; }
            public string MonthName
            {
                get
                {
                    return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.Month);
                }
            }
            public int Total { get; set; }
        
    }
}
