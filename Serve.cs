using System;
using System.Collections.Generic;

namespace ConsoleApplication10
{
    public class Serve
    {
        public List<bool> ServeLog = new List<bool>();
        public bool Winner { private set; get; } // private set 

        public Serve GetRandomServe(Random rnd) // test tool
        {
            List<bool> tmp = new List<bool>();

            for (int i = 0; i < rnd.Next(2, 15); i++)
            {
                tmp.Add(rnd.NextDouble() >= 0.5);
            }
            // Range 1: [0.0 ... 0.5]
            //Range 2: [0.5 ... 1.0]
            //|Range 1| = |Range 2|
            return new Serve() { ServeLog = tmp, Winner = GetWinnerOfServe(tmp) };
        }

        private static bool GetWinnerOfServe(List<bool> servelog) // test tool
        {
            return servelog[servelog.Count - 1 - 1]; // servelog.Count - 1 - 1 == winner 
        }

        public void PrintResults() // test tool
        {
            int zero = 0;
            int one = 0;
            foreach (var el in ServeLog)
            {
                Console.WriteLine(el);
                if (el.Equals(true))
                {
                    one++;
                }
                else zero++;
            }
            Console.WriteLine(String.Format("0false:{0}\t1true:{1}", zero, one));
        }


    }
}
