using System;
using System.Collections.Generic;
using System.Text;

namespace Time_TimePeriod
{
    /// <summary>
    ///  The Time structure.
    ///  Contains operators oveloadings and methods for arithmetic operations on Time strucure.
    /// </summary>
    /// <remarks>
    /// This struct can add and subtract.
    /// </remarks>
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

            byte h = Convert.ToByte(timeUnits[0]);
            byte m = Convert.ToByte(timeUnits[1]);
            byte s = Convert.ToByte(timeUnits[2]);
            if (h >= 24 || m >= 60 ||s >= 60)
            {
                throw new ArgumentOutOfRangeException();
            }

            Hours = h;
            Minutes = m;
            Seconds= s;
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

            if (h >= 24)
            {
                int hRest = h % 24;
                int hAdd = m / 60;
                if (hRest > 0 && hAdd > 0)
                {
                    h = 0;
                    h += hRest;
                }
                else if (h == 24)
                    h = 0;
            }
            if (m >= 60)
            {              
                int mRest = m % 60;
                int mAdd = s / 60;              
                if (mRest > 0 && mAdd > 0)
                {
                    h++;
                    m = 0;
                    m += mRest;
                }                                                
            }

            if (s >= 60)
            {
                int sRest = s % 60;
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

    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long periodInSec;
        public long PeriodInSec => periodInSec;
        private readonly long hours;
        public long Hours => hours;
        private readonly long minutes;
        public long Minutes => minutes;
        private readonly long seconds;
        public long Seconds => seconds;

        public TimePeriod(long hours = 0, long minutes = 0, long seconds = 0)
        {
            if (hours < 0 || minutes < 0 || seconds < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;

            long getSecFromHours = hours * 3600;
            long getSecFromMins = minutes * 60;

            periodInSec = getSecFromHours + getSecFromMins + seconds;
        }

        public TimePeriod(Time t1, Time t2)
        {
            var t = new Time();
            if (t1.CompareTo(t2) >= t2.CompareTo(t1))
            {
                t = t1 - t2;
            }
            else
                t = t2 - t1;
            

             hours = t.hours;
             minutes = t.minutes;
             seconds = t.seconds;

            periodInSec = (hours * 3600) + (minutes * 60) + seconds;
                      
        }

        public TimePeriod(string timePeriod)
        {
            string[] timeUnits = timePeriod.Split(':');

            long h = Convert.ToInt64(timeUnits[0]);
            long m = Convert.ToInt64(timeUnits[1]);
            long s = Convert.ToInt64(timeUnits[2]);
            if (h < 0 || m < 0 || s < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            hours = h * 3600;
            minutes = m * 60;
            seconds = s;

            periodInSec = h + m + s;
            
        }
        public override string ToString()
        {
            return $"{hours:0}:{minutes:00}:{seconds:00}";
        }

        public override bool Equals(Object obj) => (obj is TimePeriod timePeriod && Equals(timePeriod));

        public bool Equals(TimePeriod other) =>
            Hours.Equals(other.Hours) &&
            Minutes.Equals(other.Minutes) &&
            Seconds.Equals(other.Seconds) &&
            periodInSec.Equals(other.periodInSec);

        public override int GetHashCode()
        {
            return hours.GetHashCode() + minutes.GetHashCode() + seconds.GetHashCode() + periodInSec.GetHashCode();
        }

        public int CompareTo(TimePeriod other) => CompareByHours(other);

        public int CompareByHours(TimePeriod other)
        {
            if (this.Hours == other.Hours) return CompareByMinutes(other);
            return this.Hours > other.Hours ? 1 : -1;
        }

        public int CompareByMinutes(TimePeriod other)
        {
            if (this.Minutes == other.Minutes) return CompareBySeconds(other);
            return this.Minutes > other.Minutes ? 1 : -1;
        }

        public int CompareBySeconds(TimePeriod other)
        {
            if (this.Seconds == other.Seconds) return CompareByPeriodInSec(other);
            return this.Seconds > other.Seconds ? 1 : -1;
        }

        private int CompareByPeriodInSec(TimePeriod other)
        {
            if (this.periodInSec == other.periodInSec) return 0;
            return this.periodInSec > other.PeriodInSec ? 1 : -1;
        }

        public static bool operator ==(TimePeriod tP1, TimePeriod tP2) => tP1.Equals(tP2);
        public static bool operator !=(TimePeriod tP1, TimePeriod tP2) => !tP1.Equals(tP2);
        public static bool operator <(TimePeriod tP1, TimePeriod tP2) => tP1.CompareTo(tP2) < 0;
        public static bool operator >(TimePeriod tP1, TimePeriod tP2) => tP1.CompareTo(tP2) > 0;
        public static bool operator <=(TimePeriod tP1, TimePeriod tP2) => tP1.CompareTo(tP2) <= 0;
        public static bool operator >=(TimePeriod tP1, TimePeriod tP2) => tP1.CompareTo(tP2) >= 0;

        public static TimePeriod operator +(TimePeriod tP1, TimePeriod tP2)
        {
            long h = tP1.hours + tP2.hours;
            long m = tP1.minutes + tP2.minutes;
            long s = tP1.seconds + tP2.seconds;
            long period = tP1.periodInSec + tP2.periodInSec;

            return new TimePeriod(h, m, s);
        }

        public static TimePeriod operator -(TimePeriod tP1, TimePeriod tP2)
        {
            long h, m, s, period;
            if (tP1.CompareTo(tP2) > tP2.CompareTo(tP2))
            {
                h = tP1.hours - tP2.hours;
                m = tP1.minutes - tP2.minutes;
                s = tP1.seconds - tP2.seconds;
                period = tP1.periodInSec - tP2.periodInSec;
            }
            else
            {
                h = tP2.hours - tP1.hours;
                m = tP2.minutes - tP1.minutes;
                s = tP2.seconds - tP1.seconds;
                period = tP2.periodInSec - tP1.periodInSec;
            }
            return new TimePeriod(h, m, s);
        }
    }
}
