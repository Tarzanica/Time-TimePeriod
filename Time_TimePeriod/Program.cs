using System;
using System.Collections.Generic;

namespace Time_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Time> times = new List<Time>();
            times.Add(new Time(8, 30));
            times.Add(new Time(11, 23, 6));
            times.Add(new Time(22, 46, 59));
            times.Add(new Time(1, 1, 1));
            times.Add(new Time(13, 18));
            times.Add(new Time(2));
            times.Add(new Time());

            foreach (var time in times)
            {
                Console.WriteLine($"Time: {time}");
            }

            Console.WriteLine("------------------------------------");

            Time t1 = new Time(23, 34, 0);
            Console.WriteLine($"Time: {t1}");

            Time t2 = new Time(23, 34);
            Console.WriteLine($"Time: {t2}");

            //Time t3 = new Time(10,72,90);
            //Console.WriteLine($"Time: {t3}");

            Console.WriteLine(t1.Equals(t2));

        }
    }
}
