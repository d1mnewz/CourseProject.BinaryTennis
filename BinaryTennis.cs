using System;
using System.Collections.Generic;
namespace ConsoleApplication10
{
    


    // to think about realization (arduino , android, microphone etc etc etc)
    // to handle 20 21 situation 
    // to make ui 
    class Program
    {
        static void Main()
        {
            
            BinaryTennis obj = new BinaryTennis();
            obj.PlayRandomGame();
            
            obj.PrintResultOfGame();

            Console.ReadLine();
        }
    }
    public class BinaryTennis
    {

        // plyaer zero table hit == zero == false;
        // player one table hit == one = true;
        public BinaryTennis()
        {
            rnd = new Random();
        }
        Random rnd;
        public List<Serve> results = new List<Serve>();
        public int PlayerOneScore;
        public int PlayerZeroScore;
        public void AddServe(Serve obj)
        {
            if (obj.Winner.Equals(false))
            {
                PlayerZeroScore++;
            }
            if (obj.Winner.Equals(true))
            {
                PlayerOneScore++;
            }
            results.Add(obj);
        }
        public void PlayRandomGame()
        {
            for (int i = 0; PlayerOneScore != 21 && PlayerZeroScore != 21; i++)
            {
                AddServe(new Serve().GetRandomServe(rnd));
            }
           


        }
        public void PrintResultOfGame()
        {
            Console.WriteLine(String.Format("Player 0 : {0} \t Player 1 : {1}", PlayerZeroScore, PlayerOneScore));
        }

    }

    public class Serve
    {
        public List<bool> ServeLog = new List<bool>();
        public bool Winner { private set; get; }
        
        public Serve GetRandomServe(Random rnd)
        {
            List<bool> tmp = new List<bool>();
            tmp.Add(rnd.NextDouble() >= 0.5);
            tmp.Add(rnd.NextDouble() >= 0.5);
            tmp.Add(rnd.NextDouble() >= 0.5);
            tmp.Add(rnd.NextDouble() >= 0.5);
            tmp.Add(rnd.NextDouble() >= 0.5);
            tmp.Add(rnd.NextDouble() >= 0.5);
            tmp.Add(rnd.NextDouble() >= 0.5);
            // Range 1: [0.0 ... 0.5]
            //Range 2: [0.5 ... 1.0]
            //|Range 1| = |Range 2|
            return new Serve() { ServeLog = tmp, Winner = GetWinnerOfServe(tmp) };
        }

        private static bool GetWinnerOfServe(List<bool> servelog) // test tool
        {
            return servelog[servelog.Count - 1 - 1];
        }
        private void SetWinnerOfServe()
        { 
            int @false = 0;
            foreach (var el in ServeLog)
            {
                if (el.Equals(false))
                    @false++;
            }
            if (@false > (int)ServeLog.Count / 2) // to add 20:21 situation handle
            {
                Winner = false;
            }
            else Winner = true;
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
