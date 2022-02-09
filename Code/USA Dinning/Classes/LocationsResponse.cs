using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USA_Dinning.Classes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Mon
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Tue
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Wed
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Thu
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Fri
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Sat
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Sun
    {
        public string Open { get; set; }
        public string Close { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string IsOpen { get; set; }
        public string Address { get; set; }
        public Mon Mon { get; set; }
        public Tue Tue { get; set; }
        public Wed Wed { get; set; }
        public Thu Thu { get; set; }
        public Fri Fri { get; set; }
        public Sat Sat { get; set; }
        public Sun Sun { get; set; }
        public string Group { get; set; }
        public string BackgroundImage { get; set; }
    }


    public class LocationsResponse
    {
        public List<Location> locations { get; set; }
    }

}
