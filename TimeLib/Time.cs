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

        public static Time operator +(Time t1, Time t2)
        {
            int h = t1.Hours + t2.Hours;
            int m = t1.Minutes + t2.Minutes;
            int s = t1.Seconds + t2.Seconds;

            if (h > 24 || m > 60 || s > 60)
            {
                int hRest = h % 24; // add rest 
                int mRest = m % 60;
                int sRest = s % 60;

                int hAdd = m / 60; // add another hour
                int mAdd = s / 60;

                if (hRest > 0 && hAdd > 0)
                {
                    h = 0;
                    h += hRest;
                }

                if (mRest > 0 && mAdd > 0)
                {
                    h++;
                    m = 0;
                    m += mRest;
                }

                if (sRest > 0)
                {
                    m++;
                    s = 0;
                    s += sRest;
                }
            }
            return new Time((byte)h, (byte)m, (byte)s);
        }

        public static Time operator -(Time t1, Time t2)
        {
            int h, m, s;
            if (t1.CompareTo(t2) >= t2.CompareTo(t1))
            {
                 h = t1.Hours - t2.Hours;
                 m = t1.Minutes - t2.Minutes;
                 s = t1.Seconds - t2.Seconds;
            }
            else
            {
                 h = t2.Hours - t1.Hours;
                 m = t2.Minutes - t1.Minutes;
                 s = t2.Seconds - t1.Seconds;

                if (h > 0)
                    h = 24 - h;

                if (m > 0)
                {
                    m = 60 - m;
                    h--;
                }
                else
                    m = 0 - m;
                      
                if (s > 0)
                {
                    s = 60 - s;
                    m--;
                }
                else
                    s = 0 - s;

                    

            }
                      
            return new Time((byte)h, (byte)m, (byte)s);
        }

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
