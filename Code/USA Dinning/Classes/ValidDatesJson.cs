using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusDish
{
    public class ValidDatesJson
    {
        public int NumberDaysAfter { get; set; }
        public int NumberDaysBefore { get; set; }
        public List<DateTime> AvailableDates { get; set; }
        public List<object> AvailableDatesFoodOrder { get; set; }
        public DateTime Now { get; set; }
    }
}
