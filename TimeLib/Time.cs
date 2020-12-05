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
            return hours.GetHashCode() * minutes.GetHashCode() * seconds.GetHashCode();
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

            int finalSeconds = s % 60;
            int minuteToAdd = s / 60;
            int finalMinutes = (m + minuteToAdd) % 60;
            int hourToAdd = (m + minuteToAdd) / 60;
            int finalHours = (h + hourToAdd) % 24;
            return new Time((byte)finalHours, (byte)finalMinutes, (byte)finalSeconds);
        }
       
        public static Time operator -(Time t1, Time t2)
        {
            int h = t1.Hours - t2.Hours;
            int m = t1.Minutes - t2.Minutes;
            int s = t1.Seconds - t2.Seconds;
            if (s < 0)
            {
                m--;
                s  += 60;
            }
            if(m < 0)
            {
                h--;
                m += 60;
            }
            if (h < 0) h = 24 + h;
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

            if (minutes > 60 || seconds > 60)
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
            Time t = t1 - t2;
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
            if (m > 60 || s > 60)
            {
                throw new ArgumentOutOfRangeException();
            }

            hours = h;
            minutes = m;
            seconds = s;

            periodInSec = (hours * 3600) + (minutes * 60) + seconds;
            
        }
        public override string ToString()
        {
            return $"{hours:0}:{minutes:00}:{seconds:00}";
        }

        public override bool Equals(Object obj) => (obj is TimePeriod timePeriod && Equals(timePeriod));

        public bool Equals(TimePeriod other) =>
            Hours.Equals(other.Hours) &&
            Minutes.Equals(other.Minutes) &&
            Seconds.Equals(other.Seconds);

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
            if (this.Seconds == other.Seconds) return 0;
            return this.Seconds > other.Seconds ? 1 : -1;
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

            long finalSeconds = s % 60;
            long minuteToAdd = s / 60;
            long finalMinutes = (m + minuteToAdd) % 60;
            long hourToAdd = (m + minuteToAdd) / 60;
            long finalHours = h + hourToAdd;

            return new TimePeriod(finalHours, finalMinutes, finalSeconds);
        }

        public static TimePeriod operator -(TimePeriod tP1, TimePeriod tP2)
        {
            long h, m, s;
            if (tP1.CompareTo(tP2) > tP2.CompareTo(tP1))
            {
                h = tP1.hours - tP2.hours;
                m = tP1.minutes - tP2.minutes;
                s = tP1.seconds - tP2.seconds;
            }
            else
            {
                h = tP2.hours - tP1.hours;
                m = tP2.minutes - tP1.minutes;
                s = tP2.seconds - tP1.seconds;
            }

            if (s < 0)
            {
                m--;
                s += 60;
            }
            if (m < 0)
            {
                h--;
                m += 60;
            }
            if (h < 0) h += h;

            return new TimePeriod(h, m, s);
            /*
            long h = tP1.hours - tP2.hours;
            long m = tP1.minutes - tP2.minutes;
            long s = tP1.seconds - tP2.seconds;
            return new TimePeriod(h, m, s);
            */
        }
    }
}
