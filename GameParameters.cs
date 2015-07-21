using System;
using System.Collections.Generic;

namespace ConsoleApplication10
{
    public class GameParameters
    {
        public enum FinishScore
        { Score21 = 0, Score11 };
        public enum ServesForOnePlayer
        { Serves2, Serves5 };

        public FinishScore FinishScoreValue;

        public ServesForOnePlayer ServesForOnePlayerValue;

        public GameParameters()
        {
            this.FinishScoreValue = FinishScore.Score21;
            this.ServesForOnePlayerValue = ServesForOnePlayer.Serves5;
        }

        public GameParameters(FinishScore arg, ServesForOnePlayer arg2)
        {
            this.FinishScoreValue = arg;
            this.ServesForOnePlayerValue = arg2;
        }
        public GameParameters(int FinishScore, int ServesForOnePlayer)
        {
            switch (FinishScore)
            {
                case 11:
                    this.FinishScoreValue = GameParameters.FinishScore.Score11;
                    break;
                case 21:
                    this.FinishScoreValue = GameParameters.FinishScore.Score21;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            switch (ServesForOnePlayer)
            {
                case 2:
                    this.ServesForOnePlayerValue = GameParameters.ServesForOnePlayer.Serves2;
                    break;
                case 5:
                    this.ServesForOnePlayerValue = GameParameters.ServesForOnePlayer.Serves5;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        public Dictionary<String, int> GetDictionaryResult() // FinishScore, ServesForOnePlayer
        {
            Dictionary<String, int> result = new Dictionary<String, int>();
            switch (this.FinishScoreValue)
            {
                case FinishScore.Score11:
                    result.Add("FinishScore", 11);
                    break;
                case FinishScore.Score21:
                    result.Add("FinishScore", 21);
                    break;
            }
            switch (this.ServesForOnePlayerValue)
            {
                case ServesForOnePlayer.Serves2:
                    result.Add("ServesForOnePlayer", 2);
                    break;
                case ServesForOnePlayer.Serves5:
                    result.Add("ServesForOnePlayer", 5);
                    break;
            }

            return result;

        }
    }
}
