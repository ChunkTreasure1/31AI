using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _31AI.AI
{
    class MyPlayer : Player
    {
        public MyPlayer()
        {
            Name = "HackerMan";
        }

        //Called when the enemy knocks
        public override bool Knacka(int round)
        {
            //If the round is less than or equal to 3
            if (round <= 3)
            {
                //If the game score is 25 or above
                if (Game.Score(this) >= 25)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //If the round is equal to 4
            else if (round == 4)
            {
                //If the game score is 26 or greater
                if (Game.Score(this) >= 26)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //If the round is 5
            else if (round == 5)
            {
                //If the score is 27 or above
                if (Game.Score(this) >= 27)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //If the round is 6
            else if (round == 6)
            {
                //If the score is 28 or above
                if (Game.Score(this) >= 28)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //If the round is 7
            else if (round == 7)
            {
                //If the score is 29 or above
                if (Game.Score(this) >= 29)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //If the round is 8 or above
            else if (round >= 8)
            {
                //If the score is 30 or above
                if (Game.Score(this) >= 30) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Called when the AI takes up a card
        public override bool TaUppKort(Card card)
        {
            if (card.Value == 11 || (card.Value == 10 && card.Suit == BestSuit))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Called when the AI should throw a cards
        public override Card KastaKort()
        {

            Game.Score(this);
            Card worstCard = Hand.First();
            for (int i = 1; i < Hand.Count; i++)
            {
                if (Hand[i].Value < worstCard.Value)
                {
                    worstCard = Hand[i];
                }
            }
            return worstCard;
        }

        //Called every time a game has ended
        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }
        }
    }
}
