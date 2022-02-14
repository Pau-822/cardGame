using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardGame
{
    class Program
    {
        List<string> cards = new List<string>();

        //create deck of 52 cards
        private string[] cardDeckInit()
        {
            // Indicated rank goes from A to 10 and J, Q, and K.
            // Indicated suit S:Spades, H:Hearts, D:Diamonds, C:Clubs
            string[] cards = { "AS", "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "10S", "JS", "QS", "KS",
            "AH", "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "10H", "JH", "QH", "KH",
            "AD", "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "10D", "JD", "QD", "KD",
            "AC", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "JC", "QC", "KC"};
            return cards;
        }

        // shuffle the deck
        private void shuffleCardDeck(int n, List<string> cards)
        {

            for (int t = 0; t < n; t++)
            {
                Random rng = new Random();
                // empeiza el for
                for (int i = 0; i < cards.Count; i++)
                {
                    int r = rng.Next(cards.Count);
                    string aux = cards[i];
                    cards[i] = cards[r];
                    cards[r] = aux;
                }

            }

        }

        // hand cards to players
        private string[] cardHand(int n)
        {
            string[] cardsPlayer = new string[n];
            int i = 0;
            foreach (string card in cards)
            {
                cardsPlayer[i] = card;
                i++;
                if (i >= n)
                {
                    break;
                }
            }
            cards.RemoveRange(0, 5);

            return cardsPlayer;
        }

        private string printCardDeck(string[] cards)
        {
            string c = "";
            Console.WriteLine("----------------- ");
            for (int i = 0; i < cards.Length; i++)
            {
                c += cards[i] + " ";
                Console.Write(cards[i] + " ");
            }
            Console.WriteLine("\n----------------- ");
            return c;
        }

        private string getSuit(string card)
        {
            return card.Substring(card.Length - 1);
        }

        private int getRank(string card)
        {
            string r = card.Substring(0, card.Length - 1);
            if (r.Equals("J"))
            {
                return 11;

            }
            if (r.Equals("Q"))
            {
                return 12;

            }
            if (r.Equals("K"))
            {
                return 13;

            }
            if (r.Equals("A"))
            {
                return 14;

            }
            return int.Parse(r);
        }
        private string[] orderHand(string[] playerHand)
        {
            // try to improve performance later
            for (int i = 0; i < playerHand.Length - 1; i++)
            {
                for (int j = 0; j < playerHand.Length - i - 1; j++)
                {

                    if (getRank(playerHand[j]) > getRank(playerHand[j + 1]))
                    {
                        string aux = playerHand[j];
                        playerHand[j] = playerHand[j + 1];
                        playerHand[j + 1] = aux;
                    }

                }
            }

            return playerHand;
        }


        private bool isFlush(string[] playerHand)
        {
            string Suit = getSuit(playerHand[0]);

            for (int i = 1; i < playerHand.Length; i++)
            {
                if (getSuit(playerHand[i]) != Suit)
                {
                    return false;
                }
            }
            return true;

        }


        private bool isStraight(string[] playerHand)
        {
            int sec = getRank(playerHand[0]);

            for (int i = 1; i < playerHand.Length; i++)
            {
                sec++;
                if (getRank(playerHand[i]) != sec)
                {
                    return false;
                }
            }
            return true;

        }

        private bool isStraightFlush(string[] playerHand)
        {

            return (isStraight(playerHand) && isFlush(playerHand));
        }

        private Score isFourOfAKind(string[] playerHand)
        {
            Score score = new Score();


            if (getRank(playerHand[0]) == getRank(playerHand[1]) && getRank(playerHand[1]) == getRank(playerHand[2]) && getRank(playerHand[2]) == getRank(playerHand[3]))
            {
                score.Evaluation = "FOUROFAKIND";
                score.HighCard = playerHand[0];
                return score;
            }

            if (getRank(playerHand[1]) == getRank(playerHand[2]) && getRank(playerHand[2]) == getRank(playerHand[3]) && getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "FOUROFAKIND";
                score.HighCard = playerHand[4];
                return score;
            }

            return score;
        }

        private Score isFullHouse(string[] playerHand)
        {
            Score score = new Score();
            if (getRank(playerHand[0]) == getRank(playerHand[1]) && getRank(playerHand[1]) == getRank(playerHand[2]) && getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "FULLHOUSE";
                score.HighCard = playerHand[1];
                return score;
            }

            if (getRank(playerHand[0]) == getRank(playerHand[1]) && getRank(playerHand[2]) == getRank(playerHand[3]) && getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "FULLHOUSE";
                score.HighCard = playerHand[3];
                return score;
            }

            return score;



        }
        private Score isThreeOfAKind(string[] playerHand)
        {
            Score score = new Score();
            if (getRank(playerHand[0]) == getRank(playerHand[1]) && getRank(playerHand[1]) == getRank(playerHand[2]))
            {
                score.Evaluation = "THREEOFAKIND";
                score.HighCard = playerHand[0];
                return score;
            }

            if (getRank(playerHand[1]) == getRank(playerHand[2]) && getRank(playerHand[2]) == getRank(playerHand[3]))
            {
                score.Evaluation = "THREEOFAKIND";
                score.HighCard = playerHand[1];
                return score;
            }

            if (getRank(playerHand[2]) == getRank(playerHand[3]) && getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "THREEOFAKIND";
                score.HighCard = playerHand[2];
                return score;
            }

            return score;
        }

        private Score isTwoPair(string[] playerHand)
        {
            Score score = new Score();
            if (getRank(playerHand[0]) == getRank(playerHand[1]) && getRank(playerHand[2]) == getRank(playerHand[3]))
            {
                score.Evaluation = "TWOPAIR";
                if (getRank(playerHand[0]) > getRank(playerHand[2]))
                {
                    score.HighCard = playerHand[0];
                    score.SecondaryHighCard = playerHand[2]; 
                }
                else {
                    score.HighCard = playerHand[2];
                    score.SecondaryHighCard = playerHand[0];
                }
                score.HighCard = playerHand[0];
                return score;
            }

            if (getRank(playerHand[0]) == getRank(playerHand[1]) && getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "TWOPAIR";
                if (getRank(playerHand[0]) > getRank(playerHand[3]))
                {
                    score.HighCard = playerHand[0];
                    score.SecondaryHighCard = playerHand[3];
                }
                else
                {
                    score.HighCard = playerHand[3];
                    score.SecondaryHighCard = playerHand[0];
                }
                score.HighCard = playerHand[0];
                return score;
            }

            if (getRank(playerHand[1]) == getRank(playerHand[2]) && getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "TWOPAIR";
                if (getRank(playerHand[1]) > getRank(playerHand[3]))
                {
                    score.HighCard = playerHand[1];
                    score.SecondaryHighCard = playerHand[3];
                }
                else
                {
                    score.HighCard = playerHand[3];
                    score.SecondaryHighCard = playerHand[1];
                }
                score.HighCard = playerHand[0];
                return score;
            }

            return score;
        }

        private Score isOnePair(string[] playerHand)
        {
            Score score = new Score();
            if (getRank(playerHand[0]) == getRank(playerHand[1]))
            {
                score.Evaluation = "ONEPAIR";
                score.HighCard = playerHand[0];
                score.SecondaryHighCard = playerHand[playerHand.Length-1];
                return score;
            }

            if (getRank(playerHand[1]) == getRank(playerHand[2]))
            {
                score.Evaluation = "ONEPAIR";
                score.HighCard = playerHand[1];
                score.SecondaryHighCard = playerHand[playerHand.Length - 1];
                return score;
            }

            if (getRank(playerHand[2]) == getRank(playerHand[3]))
            {
                score.Evaluation = "ONEPAIR";
                score.HighCard = playerHand[2];
                score.SecondaryHighCard = playerHand[playerHand.Length - 1];
                return score;
            }

            if (getRank(playerHand[3]) == getRank(playerHand[4]))
            {
                score.Evaluation = "ONEPAIR";
                score.HighCard = playerHand[3];
                score.SecondaryHighCard = playerHand[2];
                return score;
            }

            return score;
        }


        private Score scorePlayer(string[] playerHand)
        {
            Score score = new Score();
            //esto me dice cual es el nume y la pinta
            orderHand(playerHand);
            printCardDeck(playerHand);

            //aqui van las 5 cartas del jugador

            //aqui es donde tengo que pensar como se va a saber como son las reglas de la mas alta a la minima y cada cosa return un valor y sacar primero todas las reglas para saber cuantas son y como funcionan

            if (isStraightFlush(playerHand))
            {

                Console.Write("==========> Straight Flush");
                score.Value = 9;
                score.EvaluationDescription = "Straight Flush";
                score.Evaluation = "STRAIGHT FLUSH";
                score.HighCard = playerHand[playerHand.Length - 1];
                return score;
            }
            score = isFourOfAKind(playerHand);
            if (score.Evaluation.Equals("FOUROFAKIND"))
            {

                Console.Write("==========> Four of a Kind");
                score.Value = 8;
                score.EvaluationDescription = "Four of a Kind";
                return score;
            }
            score = isFullHouse(playerHand);
            if (score.Evaluation.Equals("FULLHOUSE"))
            {
                Console.Write("==========> Full House");
                score.Value = 7;
                score.EvaluationDescription = "Full House";
                return score;
            }
            if (isFlush(playerHand))
            {

                Console.Write("==========> Flush");
                score.Value = 6;
                score.EvaluationDescription = "Flush";
                score.Evaluation = "FLUSH";
                score.HighCard = playerHand[playerHand.Length - 1];
                return score;
            }
            if (isStraight(playerHand))
            {

                Console.Write("==========> Straight");
                score.Value = 5;
                score.EvaluationDescription = "Straight";
                score.Evaluation = "STRAIGHT";
                score.HighCard = playerHand[playerHand.Length - 1];
                return score;
            }
            
            score = isThreeOfAKind(playerHand);
            if (score.Evaluation.Equals("THREEOFAKIND"))
            {
                Console.Write("==========> Three of a Kind");
                score.Value = 4;
                score.EvaluationDescription = "Three of a Kind";
                return score;
            }

            score = isTwoPair(playerHand);
            if (score.Evaluation.Equals("TWOPAIR"))
            {            

                Console.Write("==========> Two Pair");
                score.Value = 3;
                score.EvaluationDescription = "Two Pair";
                return score;
            }
            score = isOnePair(playerHand);
            if (score.Evaluation.Equals("ONEPAIR"))
            { 
                Console.Write("==========> One Pair");
                score.Value = 2;
                score.EvaluationDescription = "One Pair";
                return score;
            }


            Console.Write("==========> High Card");

            score.Value = 1;
            score.EvaluationDescription = "High Card";
            score.Evaluation = "HIGHCARD";
            score.HighCard= playerHand[playerHand.Length - 1];
            return score;


        }
        static void Main(string[] args)
        {
            int shuffleTimes = 5;
            int numberGamesSameCards = 500;
            int numberTotalGames = 100;

            Program p = new Program();


            p.printCardDeck(p.cards.ToArray());




            var csv = new StringBuilder();
            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "Player 1", "Player 2", "Player 3", "Player 4", "Player 5", "Player 6", "Evaluated as", "Result", "Winning Hand", "Winning Player", "Total Wins","Wining Percentage");
            csv.AppendLine(newLine);
            string game = "";
            var txt = new StringBuilder();
            newLine = string.Format("{0}", "Summary");
            Dictionary<string, Summary> summary = new Dictionary<string, Summary>();
            for (int k = 0; k < numberTotalGames; k++)
            {
                p.cards = new List<string>();
                p.cards.AddRange(p.cardDeckInit());
                p.shuffleCardDeck(shuffleTimes, p.cards);
                List<string[]> players = new List<string[]>();



                List<int> tiePlayers = new List<int>();

                players.Add(p.cardHand(5));
                Console.WriteLine("Player 1");
                Score scorePlayer = new Score();
                Console.WriteLine("\n------------");
                int totalWins = 0;
                scorePlayer = p.scorePlayer(players[0]);
                if (!summary.ContainsKey(scorePlayer.EvaluationDescription))
                {
                    summary.Add(scorePlayer.EvaluationDescription, new Summary());
                }
                
                summary[scorePlayer.EvaluationDescription].NumberOfHands += 1;
                summary[scorePlayer.EvaluationDescription].PercentageOfHands = ((double)summary[scorePlayer.EvaluationDescription].NumberOfHands / numberTotalGames) * 100.0;
                summary[scorePlayer.EvaluationDescription].TotalGames = summary[scorePlayer.EvaluationDescription].NumberOfHands * numberGamesSameCards;
                Score player1 = scorePlayer;
                for (int j = 0; j < numberGamesSameCards; j++)
                {
                   
                    game += p.printCardDeck(players[0]) + ",";

                    int winner = 0;
                    scorePlayer = p.scorePlayer(players[0]);
                    Score winnerScore = scorePlayer;
                    string evaluated = scorePlayer.EvaluationDescription;
                    
                    bool tie = false;
                    for (int i = 1; i < 6; i++)
                    {
                        players.Add(p.cardHand(5));

                        Console.WriteLine("Player " + (i + 1));
                        game += p.printCardDeck(players[i]);
                        scorePlayer = p.scorePlayer(players[i]);

                        Console.WriteLine("scorePlayer:"+scorePlayer.Value + "WinnerScore:" + winnerScore.Value);
                        if (scorePlayer.Value >= winnerScore.Value)
                        {
                            if (scorePlayer.Value == winnerScore.Value)
                            {
                                if (p.getRank(scorePlayer.HighCard) > p.getRank(winnerScore.HighCard))
                                {
                                    winner = i;
                                    winnerScore = scorePlayer;
                                    tie = false;
                                    tiePlayers = new List<int>();
                                }
                                if ((p.getRank(scorePlayer.HighCard) == p.getRank(winnerScore.HighCard))) {

                                    if (scorePlayer.Evaluation.Equals("HIGHCARD") || scorePlayer.Evaluation.Equals("FLUSH"))
                                    {

                                        for (int l = players[i].Length - 1; l >= 0; l--)
                                        {

                                            if (p.getRank(players[i][l]) > p.getRank(players[winner][l]))
                                            {
                                                winner = i;
                                                winnerScore = scorePlayer;
                                                tie = false;
                                                tiePlayers = new List<int>();
                                                break;
                                            }
                                            if (p.getRank(players[i][l]) < p.getRank(players[winner][l]))
                                            {
                                                break;
                                            }
                                        }

                                        tie = true;
                                        tiePlayers.Add(i);
                                    }
                                    else {
                                        if (scorePlayer.Evaluation.Equals("ONEPAIR") || scorePlayer.Evaluation.Equals("TWOPAIR"))
                                        {
                                            if (p.getRank(scorePlayer.SecondaryHighCard) > p.getRank(winnerScore.SecondaryHighCard))
                                            {
                                                winner = i;
                                                winnerScore = scorePlayer;
                                                tie = false;
                                                tiePlayers = new List<int>();
                                            }

                                        }
                                        else
                                        {
                                            tie = true;
                                            tiePlayers.Add(i);
                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                winner = i;
                                winnerScore = scorePlayer;
                                tie = false;
                                tiePlayers = new List<int>();
                            }
                        }
                        Console.WriteLine("\n------------");


                        game += ",";

                    }
                    game += evaluated + ",";
                 
                    if (winner == 0)
                    {
                        game += "WIN,";
                        totalWins++;
                    }
                    else
                    {
                        game += "LOOSE,";
                    }
                    double winPercentage =((double)totalWins / numberGamesSameCards )* 100.0;

                    game += winnerScore.EvaluationDescription + ",";
                    game += "Player " + (winner + 1) + ",";
                    game += totalWins + ",";
                    game += winPercentage + "%,";
                    csv.AppendLine(game);


                    if (!tie)
                    {
                        Console.WriteLine("Winner:  Player" + (winner + 1));
                    }
                    else
                    {
                        tiePlayers.Add(winner);
                        Console.WriteLine("TIE");
                    }

                    p.printCardDeck(p.cards.ToArray());
                    for (int i = 1; i < 6; i++)
                    {
                        p.cards.AddRange(players[i]);

                    }
                    players.RemoveRange(1, 5);
                    p.shuffleCardDeck(shuffleTimes, p.cards);
                    game = "";
                }
                summary[player1.EvaluationDescription].OverallWin += totalWins;
                summary[player1.EvaluationDescription].OveralPercentage = ((double)totalWins/ summary[player1.EvaluationDescription].TotalGames)*100.0;
            }

            File.WriteAllText("report.csv", csv.ToString());
            string reportSummary = "";
            foreach (var reg in summary) {

                reportSummary += "\n\nRank: " + reg.Key + "\nNumber of Hands:" + reg.Value.NumberOfHands+ "\tPercentage of Hands:"+reg.Value.PercentageOfHands+"\tOverall Winning Hands:"+reg.Value.OverallWin+"\tTotal Played:"+reg.Value.TotalGames + "\tPercentage Winning hands:" + reg.Value.OveralPercentage;
            
            }
            txt.Append(reportSummary);
            File.WriteAllText("summary.txt", txt.ToString());

        }
    }
}
