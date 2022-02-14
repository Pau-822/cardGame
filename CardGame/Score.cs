using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    class Score
    {
        int value = 0;
        string evaluation="";
        string evaluationDescription;
        string highCard;
        string secondaryHighCard;

        public int Value { get => value; set => this.value = value; }
        public string Evaluation { get => evaluation; set => evaluation = value; }
        public string SecondaryHighCard { get => secondaryHighCard; set => secondaryHighCard = value; }
        public string HighCard { get => highCard; set => highCard = value; }
        public string EvaluationDescription { get => evaluationDescription; set => evaluationDescription = value; }
    }
}
