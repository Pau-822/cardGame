using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    class Summary
    {
        int numberOfHands = 0;
        double percentageOfHands = 0;
        int overallWin = 0;
        double overalPercentage = 0;
        int totalGames = 0;
        public double OveralPercentage { get => overalPercentage; set => overalPercentage = value; }
        public int NumberOfHands { get => numberOfHands; set => numberOfHands = value; }
        public double PercentageOfHands { get => percentageOfHands; set => percentageOfHands = value; }
        public int OverallWin { get => overallWin; set => overallWin = value; }
        public int TotalGames { get => totalGames; set => totalGames = value; }
    }
}
