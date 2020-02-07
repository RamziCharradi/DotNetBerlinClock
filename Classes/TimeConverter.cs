using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
       

        /// <summary>
        /// Convert string time format HH:mm:ss into Berlin clock time.
        /// </summary>
        /// <param name="aTime">Time to convert. Format HH:mm:ss</param>
        /// <returns>Berlin clock time</returns>
        public string convertTime(string aTime)
        {
            // Empty parameter.
            if (string.IsNullOrWhiteSpace(aTime))
                throw new ArgumentException("Empty time");

            string[] split = aTime.Split(':');

            // check time
            if (!IsValidTime(split))
            {
                throw new Exception("Time is invalid");
            }

           
            return ConvertTimeToBerlinTime(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
        }


        bool IsValidTime(string[] split)
        {
            bool isValidTime = true;
            int hours, minutes, seconds;

            // Format incorrect. Expected: HH:mm:ss.
            if (split.Length != 3)
                isValidTime=false;
            // hours values and range
            if (!int.TryParse(split[0], out hours) && (hours < 0 || hours > 24))
                isValidTime = false;
            // minutes values and range
            if (!int.TryParse(split[1], out minutes) && (minutes < 0 || minutes > 59))
                isValidTime = false;
            // Secondes values and range
            if (!int.TryParse(split[2], out seconds) && (seconds < 0 || seconds > 59))
                isValidTime = false;

            return isValidTime;
        }


        
        public string ConvertTimeToBerlinTime(int hours, int minutes, int seconds)
        {
            // line 1: blink on yellow Y if seconds is an even number else Off.
            string berlinTime = (seconds % 2 == 0) ? "Y" : "O";
            int n = hours / 5;

            // line 2: 
            // 5 hours lamps.
            berlinTime += Environment.NewLine + new String('R', n).PadRight(4, 'O');

            // line 3: 
            // 1 hour lamps.
            n = hours - (n * 5);
            berlinTime += Environment.NewLine + new String('R', n).PadRight(4, 'O');

            // line 4: 
            // 5 minutes lamps.
            n = minutes / 5;
            berlinTime += Environment.NewLine + new String('Y', n).PadRight(11, 'O').Replace("YYY", "YYR");

            // line 5: 
            //  1 minute lamp.
            n = minutes - (n * 5);
            berlinTime += Environment.NewLine + new String('Y', n).PadRight(4, 'O');

            return berlinTime;
        }


    }
}