using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class DateTimeJson 
    {
#region Time conversions
        //private static DateTime ConvertFromUnixTimestamp(double timestamp)
        //{

        //    var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        //    return origin.AddSeconds(timestamp);

        //}

        //private long ToUnixTimespan(DateTime date)
        //{
        //    TimeSpan tspan = date.ToUniversalTime().Subtract(
        //       new DateTime(1970, 1, 1, 0, 0, 0));

        //    return (long)Math.Truncate(tspan.TotalSeconds);
        //}

        //private string Duration(Station firstStation, Station secondStation)
        //{
        //    int duration1 = firstStation.duration;
        //    int duration2 = secondStation.duration;

        //    int result = duration2 - duration1;

        //    String startTime1 = "00:00";
        //    int minutes = result;
        //    int h = minutes / 60 + Int32.Parse(startTime1.Substring(0, 1));
        //    int m = minutes % 60 + Int32.Parse(startTime1.Substring(3, 4));
        //    String newtime = h + ":" + m;
        //    return newtime;
            
        //}

        //private string DepartureConverter(Station input)
        //{
        //    Int32 duration = input.duration;


        //    String startTime = "00:00";
        //    int minutes = duration;
        //    int h = minutes / 60 + Int32.Parse(startTime.Substring(0, 1));
        //    int m = minutes % 60 + Int32.Parse(startTime.Substring(3, 4));
        //    String newtime = h + ":" + m;
        //    return newtime;
        //}
        //private DateTime ArrivalConverter(Station input2)
        //{
        //   string arrival = input2.arrives;
        //    DateTime a = Convert.ToDateTime(arrival);
        //    return a;
        //}

       

#endregion
    }
}
