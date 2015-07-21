using System;
using System.Collections.Generic;
namespace ConsoleApplication10
{

    // to think about realization (arduino , android, microphone, infrared sensor etc etc etc)
    // to make ui 
    
    class Program
    {
        static void Main()
        {

            try
            {

                BinaryTennis obj = new BinaryTennis(new GameParameters(11, 2));
                obj.PlayRandomGame();

                obj.PrintResultOfGame();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid Game Parameters.");
            }
            Console.ReadLine();
        }
    }
    public class BinaryTennis
    {
        // plyaer zero table hit == zero == false;
        // player one table hit == one = true;
        public BinaryTennis(GameParameters @params)
        {
            rnd = new Random();
            this.CriticalScore = false;
            this.PlayerOneScore = 0;
            this.PlayerZeroScore = 0;
            this.@params = @params.GetDictionaryResult();
        }
        public Dictionary<String, int> @params;
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
            CriticalScores PlayerZeroResult = 0; // start from equal
            
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
            for (int i = 0; PlayerOneScore != this.@params["FinishScore"] && PlayerZeroScore != this.@params["FinishScore"]; i++)
            {
                if (this.CriticalScore == true)
                {
                    CriticalScoreHandle();
                    break; 
                }
                AddServe(new Serve().GetRandomServe(rnd));
                if (this.PlayerOneScore.Equals(this.@params["FinishScore"] - 1) && this.PlayerZeroScore.Equals(this.@params["FinishScore"] - 1))
                {
                    this.CriticalScore = true;
                }
            }

            // test part for criticalScore ; it generates 20: 20 score
            // to make this possible make Serve.winner set as non private
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
}