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
            this.CriticalScore = false;
            this.PlayerOneScore = 0;
            this.PlayerZeroScore = 0;
        }
        Random rnd;
        public List<Serve> results = new List<Serve>();
        public int PlayerOneScore;
        public int PlayerZeroScore;
        public bool CriticalScore; // to handle 20-20 / 10-10 / etc situation

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

        enum CriticalScores
        {
            Lose = -2, Less, Equal, More, Win
        }
        public void CriticalScoreHandle()
        {
            CriticalScores PlayerZeroResult = 0;
            
            Serve tmpServe;
            for (int i = 0; ; i++)
            {
                tmpServe = new Serve().GetRandomServe(rnd);
                AddServe(tmpServe);
                switch (tmpServe.Winner)
                {
                   case false:
                        PlayerZeroResult += 1;
                        if (PlayerZeroResult == CriticalScores.Win)
                            return;
                        break;
                    case true:
                        PlayerZeroResult -= 1;
                        if (PlayerZeroResult == CriticalScores.Lose)
                            return;
                        break;
                }

                
            }
        }
        public void PlayRandomGame()
        {
            for (int i = 0; PlayerOneScore != 21 && PlayerZeroScore != 21; i++)
            {
                if (this.CriticalScore == true)
                {
                    CriticalScoreHandle();
                    break;
                }
                AddServe(new Serve().GetRandomServe(rnd));
                if (this.PlayerOneScore.Equals(20) && this.PlayerZeroScore.Equals(20))
                {
                    this.CriticalScore = true;
                }
            }

            // test part for criticalScore ; it generates 20: 20 score
            // 
            //for (int i = 0; PlayerOneScore != 21 && PlayerZeroScore != 21; i++)
            //{
            //    if (this.CriticalScore == true)
            //    {
            //        CriticalScoreHandle();
            //        break;
            //    }
            //    AddServe(new Serve() { Winner = Convert.ToBoolean((i & 1) == 0), ServeLog = new List<bool>() });
            //    if (this.PlayerOneScore.Equals(20) && this.PlayerZeroScore.Equals(20))
            //    {
            //        this.CriticalScore = true;
            //    }

            //}
        }

        public void PrintResultOfGame()
        {
            Console.WriteLine(String.Format("Player 0 : {0} \t Player 1 : {1}", PlayerZeroScore, PlayerOneScore));
        }

    }

    public class Serve
    {
        public List<bool> ServeLog = new List<bool>();
        public bool Winner {set; get; }
        
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
