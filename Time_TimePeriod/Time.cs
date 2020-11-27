using System;
using System.Collections.Generic;
using System.Text;

namespace Time_TimePeriod
{
    public struct Time
    {
        public readonly byte Hours;
        public byte hours => 24;
        public readonly byte Minutes;
        public byte minutes => 60;
        public readonly byte Seconds;
        public byte seconds => 60;

        public Time(byte hours, byte minutes, byte seconds)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public Time(byte hours, byte minutes)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = 0;
        }

        public Time(byte hours)
        {
            this.Hours = hours;
            this.Minutes = 0;
            this.Seconds = 0;
        }

        public Time(string time)
        {
            this.Hours = time;
            this.Minutes = time;
            this.Seconds = time;
        }
    }
}
