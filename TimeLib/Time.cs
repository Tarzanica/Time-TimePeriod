using System;
using System.Collections.Generic;
using System.Text;

namespace Time_TimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public readonly byte Hours;
        public byte hours => Hours;
        public readonly byte Minutes;
        public byte minutes => Minutes;
        public readonly byte Seconds;
        public byte seconds => Seconds;

        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0)
        {
            if ((hours >= 24 || minutes >= 60 || seconds >= 60))
                throw new ArgumentOutOfRangeException();

            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public Time(string time)
        {
            string[] timeUnits = time.Split(':');

            byte a = Convert.ToByte(timeUnits[0]);
            byte b = Convert.ToByte(timeUnits[1]);
            byte c = Convert.ToByte(timeUnits[2]);
            if (a >= 24 || b >= 60 || c >= 60)
            {
                throw new ArgumentOutOfRangeException();
            }

            Hours = a;
            Minutes = b;
            Seconds= c;
        }

        public override string ToString()
        {
            return $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }

        public override bool Equals(Object obj) => (obj is Time time && Equals(time));

        public bool Equals(Time other) =>
            Hours.Equals(other.Hours) &&
            Minutes.Equals(other.Minutes) &&
            Seconds.Equals(other.Seconds);

        public override int GetHashCode()
        {
            return hours.GetHashCode() + minutes.GetHashCode() + seconds.GetHashCode();
        }

        public int CompareTo(Time other) => CompareByHours(other);

        public int CompareByHours(Time other)
        {
            if (this.Hours == other.Hours) return CompareByMinutes(other);
            return this.Hours > other.Hours ? 1 : -1;
        }

        public int CompareByMinutes(Time other)
        {
            if (this.Minutes == other.Minutes) return CompareBySeconds(other);
            return this.Minutes > other.Minutes ? 1 : -1;
        }

        public int CompareBySeconds(Time other)
        {
            if (this.Seconds == other.Seconds) return 0;
            return this.Seconds > other.Seconds ? 1 : -1;
        }

        //operators overloading

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !t1.Equals(t2);
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;

        //public static bool operator +(Time t1, Time t2)
        //{

        //}
        //public Time Plus(TimePeriod)
        //{

        //}
    }

    public struct TimePeriod
    {
        private readonly long periodInSec;
        public long PeriodInSec => periodInSec;
    }
}
