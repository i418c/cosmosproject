using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware
{
    public class Time
    {
        //Always 24 time mode
        public static int getHour()
        {
            return Cosmos.HAL.RTC.Hour;
        }

        public static int getMinute()
        {
            return Cosmos.HAL.RTC.Minute;
        }

        public static int getSecond()
        {
            return Cosmos.HAL.RTC.Second;
        }

        public static int getDay()
        {
            return Cosmos.HAL.RTC.DayOfTheMonth;
        }

        public static int getMonth()
        {
            return Cosmos.HAL.RTC.Month;
        }

        public static int getYear()
        {
            return Cosmos.HAL.RTC.Year;
        }

        public static String getDate()
        {
            return getYear().ToString() + "/" + getMonth().ToString() + "/" + getDay().ToString();
        }

        public static String getTime()
        {
            return getHour().ToString() + ":" + getMinute().ToString() + ":" + getSecond().ToString();
        }

        public static string getDateTime()
        {
            return getDate() + " - " + getTime();
        }
    }
}
