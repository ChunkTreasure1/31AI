using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _31AI.AI
{
    class MyPlayer : Player
    {
        List<Card> OpponentCards = new List<Card>();

        public MyPlayer()
        {
            Name = "HackerMan";
        }

        //Called when the enemy knocks
        public override bool Knacka(int round)
        {
            //If the opponent didn't pick up a card from the pile
            if (OpponentsLatestCard != null)
            {
                //Add the card to the opponentscards list
                OpponentCards.Add(OpponentsLatestCard);
            }

            //If the round is less than or equal to 3
            if (round <= 3)
            {
                //If the game score is 25 or above
                if (Game.Score(this) >= 20)
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
                if (Game.Score(this) >= 22)
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
                if (Game.Score(this) >= 24)
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
                if (Game.Score(this) >= 26)
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
                if (Game.Score(this) >= 28)
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
            //If the card isn't null
            if (card != null)
            {
                //Check if the hand is one suit
                if (IsOneSuit())
                {
                    //If the cards suit is the best suit
                    if (card.Suit == BestSuit)
                    {
                        //Temp value
                        int lowestValue = 11;

                        //Go through the hand
                        for (int i = 0; i < Hand.Count; i++)
                        {
                            //If the hands lowest card is lower than the lowestValue
                            if (Hand[i].Value < lowestValue)
                            {
                                //Change the lowestValue
                                lowestValue = Hand[i].Value;
                            }
                        }

                        //If the value is less than the lowest value
                        if (card.Value > lowestValue)
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
                        if (card.Value == 11)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Called when the AI should throw a cards
        public override Card KastaKort()
        {
            //If all the cards in the hand is the same suit
            if (IsOneSuit())
            {
                Card lowestCard = null;
                int lowestCardValue = 11;

                //Go through the hand
                for (int i = 0; i < Hand.Count; i++)
                {
                    //If the current cards value is less than the last lowest value
                    if (Hand[i].Value < lowestCardValue)
                    {
                        //Set the values
                        lowestCard = Hand[i];
                        lowestCardValue = Hand[i].Value;
                    }
                }

                //Return the worst card
                return lowestCard;
            }
            else
            {
                //Temp values
                Card throwawayCard = null;
                int lowestCardValue = 11;

                //Go through the hand
                for (int i = 0; i < Hand.Count; i++)
                {
                    //If it's not the best suit and the value is less than the lowestCardValue
                    if (Hand[i].Suit != BestSuit && Hand[i].Value < lowestCardValue)
                    {
                        throwawayCard = Hand[i];
                    }
                }
                return throwawayCard;
            }
        }

        //Called every time a game has ended
        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

            //Clear the collecting cache
            OpponentCards.Clear();
        }

        //Check if the player has full hand of one suit
        bool IsOneSuit()
        {
            Game.Score(this);

            int numCards = 0;

            //Go through all the cards
            for (int i = 0; i < Hand.Count; i++)
            {
                //If the suit if the current card is the best suit
                if (Hand[i].Suit == BestSuit)
                {
                    //Increase the temp num
                    numCards++;
                }
            }

            //If the amount of cards is equal to the amount of cards that is of the best suit
            if (numCards == Hand.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Gets the probable collecting suit of the opponent
        Suit GetPropableOpponentCollectingSuit()
        {
            //int array to hold the amounts
            int[] amounts = new int[4];

            //Go through all the cards that the opponent has taken from the throwaway pile
            for (int i = 0; i < OpponentCards.Count; i++)
            {
                //Add to the correct amount
                if (OpponentCards[i].Suit == Suit.Hjärter)
                {
                    amounts[0]++;
                }
                else if (OpponentCards[i].Suit == Suit.Klöver)
                {
                    amounts[1]++;
                }
                else if (OpponentCards[i].Suit == Suit.Ruter)
                {
                    amounts[2]++;
                }
                else if (OpponentCards[i].Suit == Suit.Spader)
                {
                    amounts[3]++;
                }
            }

            //Get the index of the highest occuring number
            int maxIndex = amounts.ToList().IndexOf(amounts.Max());

            //Return the right suit based on the number
            if (maxIndex == 0)
            {
                return Suit.Hjärter;
            }
            else if (maxIndex == 1)
            {
                return Suit.Klöver;
            }
            else if (maxIndex == 2)
            {
                return Suit.Ruter;
            }
            else if (maxIndex == 3)
            {
                return Suit.Spader;
            }
            else
            {
                return Suit.Hjärter;
            }   
        }
    }

    class TestAI : Player
    {
        List<Card> OpponentCards = new List<Card>();

        public TestAI()
        {
            Name = "TestMan";
        }

        //Called when the enemy knocks
        public override bool Knacka(int round)
        {
            //If the opponent didn't pick up a card from the pile
            if (OpponentsLatestCard != null)
            {
                //Add the card to the opponentscards list
                OpponentCards.Add(OpponentsLatestCard);
            }

            //If the round is less than or equal to 3
            if (round <= 3)
            {
                //If the game score is 25 or above
                if (Game.Score(this) >= 20)
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
                if (Game.Score(this) >= 22)
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
                if (Game.Score(this) >= 24)
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
                if (Game.Score(this) >= 26)
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
                if (Game.Score(this) >= 28)
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
            //If the card isn't null
            if (card != null)
            {
                //Check if the hand is one suit
                if (IsOneSuit())
                {
                    //If the cards suit is the best suit
                    if (card.Suit == BestSuit)
                    {
                        //Temp value
                        int lowestValue = 11;

                        //Go through the hand
                        for (int i = 0; i < Hand.Count; i++)
                        {
                            //If the hands lowest card is lower than the lowestValue
                            if (Hand[i].Value < lowestValue)
                            {
                                //Change the lowestValue
                                lowestValue = Hand[i].Value;
                            }
                        }

                        //If the value is less than the lowest value
                        if (card.Value > lowestValue)
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
                        if (card.Value == 11)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Called when the AI should throw a cards
        public override Card KastaKort()
        {
            //If all the cards in the hand is the same suit
            if (IsOneSuit())
            {
                Card lowestCard = null;
                int lowestCardValue = 11;

                //Go through the hand
                for (int i = 0; i < Hand.Count; i++)
                {
                    //If the current cards value is less than the last lowest value
                    if (Hand[i].Value < lowestCardValue)
                    {
                        //Set the values
                        lowestCard = Hand[i];
                        lowestCardValue = Hand[i].Value;
                    }
                }

                //Return the worst card
                return lowestCard;
            }
            else
            {
                Card throwawayCard = null;
                int lowestCardValue = 11;

                for (int i = 0; i < Hand.Count; i++)
                {
                    if (Hand[i].Suit != BestSuit && Hand[i].Value < lowestCardValue)
                    {
                        throwawayCard = Hand[i];
                    }
                }

                return throwawayCard;
            }
        }

        //Called every time a game has ended
        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

            //Clear the collecting cache
            OpponentCards.Clear();
        }

        bool IsOneSuit()
        {
            Game.Score(this);

            int numCards = 0;

            //Go through all the cards
            for (int i = 0; i < Hand.Count; i++)
            {
                //If the suit if the current card is the best suit
                if (Hand[i].Suit == BestSuit)
                {
                    //Increase the temp num
                    numCards++;
                }
            }

            //If the amount of cards is equal to the amount of cards that is of the best suit
            if (numCards == Hand.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Suit GetPropableOpponentCollectingSuit()
        {
            int numHeart = 0;
            int numSpade = 0;
            int numDiamond = 0;
            int numClubs = 0;

            for (int i = 0; i < OpponentCards.Count; i++)
            {
                if (OpponentCards[i].Suit == Suit.Hjärter)
                {
                    numHeart++;
                }
                else if (OpponentCards[i].Suit == Suit.Klöver)
                {
                    numClubs++;
                }
                else if (OpponentCards[i].Suit == Suit.Ruter)
                {
                    numDiamond++;
                }
                else if (OpponentCards[i].Suit == Suit.Spader)
                {
                    numSpade++;
                }
            }

            return Suit.Hjärter;
        }
    }
}
