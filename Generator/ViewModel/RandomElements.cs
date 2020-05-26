using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.ViewModel
{
    public class RandomElements
    {
        private static readonly Random random = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock(random)
            {
                return random.Next(min, max);
            }
        }

        public static double GetRandomDouble(double min, double max, int round)
        {
            double outcome = random.NextDouble() * (max - min) + min;
            return (double)decimal.Round((decimal)outcome, round, MidpointRounding.AwayFromZero);
        }

        public static string GetRandomDate(int start, int end, string format)
        {
            var randomTest = new Random();
            DateTime startDate = new DateTime(start, 1, 1);
            DateTime endDate = new DateTime(end, 1, 1);

            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;


            return newDate.ToString(format);
        }

        public static string GetRandomResult()
        {
            List<int> results = new List<int>() {0, 1, 2, 3 };

            int first = results[random.Next(0, results.Count-1)];
            int second;
            if(first != 3)
            {
                second = 3;               
            }
            else
            {
                second = results[random.Next(0, results.Count-1)];
            }

            return first.ToString() + ":" + second.ToString();
        }
    }
}
