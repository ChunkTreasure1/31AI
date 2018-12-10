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
        List<int> OpponentKnockingScore = new List<int>();
        List<int> KnockingScore = new List<int>();

        int GameNumber = 0;
        bool ShouldUseFirst = false;

        public MyPlayer()
        {
            Name = "HackerMan";
        }

        //Called when the enemy knocks
        public override bool Knacka(int round)
        {
            if (Game.Score(this) >= 23)
            {
                return true;
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
                else
                {
                    List<Card> cards = GetNonBestSuitCards();

                    if (card.Suit == cards[0].Suit)
                    {
                        int sumNonBest = 0;

                        for (int i = 0; i < cards.Count; i++)
                        {
                            sumNonBest += cards[0].Value;
                        }

                        sumNonBest += card.Value;

                        if (sumNonBest > Game.SuitScore(Hand, BestSuit))
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
                int lowestCardValue = 12;

                //Go through the hand
                for (int i = 0; i < Hand.Count; i++)
                {
                    //If it's not the best suit and the value is less than the lowestCardValue
                    if (Hand[i].Suit != BestSuit && Hand[i].Value < lowestCardValue)
                    {
                        lowestCardValue = Hand[i].Value;
                        throwawayCard = Hand[i];
                    }
                }

                if (throwawayCard == null)
                {
                    Console.Write("t");
                }
                return throwawayCard;
            }
        }

        //Called every time a game has ended
        public override void SpelSlut(bool wonTheGame)
        {
            GameNumber++;

            if (wonTheGame)
            {
                Wongames++;
            }

            if (lastTurn)
            {
                OpponentKnockingScore.Add(OpponentLatestScore);
            }

            //Clear the collecting cache
            OpponentCards.Clear();
        }

        //Check if the player has full hand of one suit
        private bool IsOneSuit()
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

        //Get a list of the cards of the suit that isn't best
        private List<Card> GetNonBestSuitCards()
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit != BestSuit)
                {
                    cards.Add(Hand[i]);
                }
            }

            int[] sums = new int[4];

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Suit == Suit.Hjärter)
                {
                    sums[0] += cards[i].Value;
                }
                else if (cards[i].Suit == Suit.Klöver)
                {
                    sums[1] += cards[i].Value;
                }
                else if (cards[i].Suit == Suit.Ruter)
                {
                    sums[2] += cards[i].Value;
                }
                else if (cards[i].Suit == Suit.Spader)
                {
                    sums[3] += cards[i].Value;
                }
            }

            int maxIndex = sums.ToList().IndexOf(sums.Max());

            Suit bestSuit;

            if (maxIndex == 0)
            {
                bestSuit = Suit.Hjärter;
            }
            else if (maxIndex == 1)
            {
                bestSuit = Suit.Klöver;
            }
            else if (maxIndex == 2)
            {
                bestSuit = Suit.Ruter;
            }
            else if (maxIndex == 3)
            {
                bestSuit = Suit.Spader;
            }
            else
            {
                bestSuit = Suit.Hjärter;
            }

            List<Card> returnCards = new List<Card>();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Suit == bestSuit)
                {
                    returnCards.Add(cards[i]);
                }
            }

            return returnCards;
        }

        //Gets the average of the opponents knocking score
        private int GetOpponentKnockingAverage()
        {
            //Get the sum and the average
            int sum = OpponentKnockingScore.Sum();
            int average = 0;

            if (OpponentKnockingScore.Count != 0)
            {
                average = sum / OpponentKnockingScore.Count;

                
            }
            else
            {
                average = 20;
            }

            return average;
        }

        private int GetKnockingAverage()
        {
            int sum = 0;

            for (int i = 0; i < KnockingScore.Count; i++)
            {
                sum += KnockingScore[i];
            }

            int average = sum / KnockingScore.Count;

            return average;
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

    class OtherAI : Player
    {
        List<int> OpponentKnockingScore = new List<int>();
        List<Card> OpponentCards = new List<Card>();
        Suit TempSuit;

        public OtherAI()
        {
            Name = "OtherAI";
        }

        public override bool Knacka(int round)
        {
            if (Game.Score(this) >= 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Called when the player is supposed to draw a card
        public override bool TaUppKort(Card card)
        {
            if (card != null)
            {
                int numberOfAces = 0;

                for (int i = 0; i < Hand.Count; i++)
                {
                    if (Hand[i].Value == 11)
                    {
                        numberOfAces++;
                    }
                }

                if (card.Value == 11 && numberOfAces > 1)
                {
                    return true;
                }
                else
                {
                    if (IsOneSuit())
                    {
                        if (GetWorstCardOfSuit(BestSuit).Value < card.Value)
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
                        List<Card> cards = GetNonBestSuitCards();

                        if (card.Suit == cards[0].Suit)
                        {
                            int sum = 0;

                            for (int i = 0; i < cards.Count; i++)
                            {
                                sum += cards[i].Value;
                            }

                            sum += card.Value;

                            if (sum > Game.Score(this))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Called when the player is supposed to throw a card
        public override Card KastaKort()
        {
            //Temp value
            Card throwaway = null;

            //Go through the hand and find the card that isn't of the best suit
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit != BestSuit)
                {
                    throwaway = Hand[i];
                }
            }

            //If the above search ends up as null
            if (throwaway == null)
            {
                //Temp value
                int lowestValue = 12;

                //Go through the hand again to find the card of the lowest value
                for (int i = 0; i < Hand.Count; i++)
                {
                    if (Hand[i].Value < lowestValue)
                    {
                        lowestValue = Hand[i].Value;
                        throwaway = Hand[i];
                    }
                }
            }
            return throwaway;
        }

        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

            if (lastTurn)
            {
                OpponentKnockingScore.Add(OpponentLatestScore);
            }
        }

        private int GetOpponentKnockingAverage()
        {
            //Get the sum and the average
            int sum = OpponentKnockingScore.Sum();
            int average = 0;

            if (OpponentKnockingScore.Count != 0)
            {
                average = sum / OpponentKnockingScore.Count;


            }
            else
            {
                average = 20;
            }

            return average;
        }

        //Check if the player has full hand of one suit
        private bool IsOneSuit()
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

        private List<Card> GetNonBestSuitCards()
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit != BestSuit)
                {
                    cards.Add(Hand[i]);
                }
            }

            int[] sums = new int[4];

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Suit == Suit.Hjärter)
                {
                    sums[0] += cards[i].Value;
                }
                else if (cards[i].Suit == Suit.Klöver)
                {
                    sums[1] += cards[i].Value;
                }
                else if (cards[i].Suit == Suit.Ruter)
                {
                    sums[2] += cards[i].Value;
                }
                else if (cards[i].Suit == Suit.Spader)
                {
                    sums[3] += cards[i].Value;
                }
            }

            int maxIndex = sums.ToList().IndexOf(sums.Max());

            Suit bestSuit;

            if (maxIndex == 0)
            {
                bestSuit = Suit.Hjärter;
            }
            else if (maxIndex == 1)
            {
                bestSuit = Suit.Klöver;
            }
            else if (maxIndex == 2)
            {
                bestSuit = Suit.Ruter;
            }
            else if (maxIndex == 3)
            {
                bestSuit = Suit.Spader;
            }
            else
            {
                bestSuit = Suit.Hjärter;
            }

            List<Card> returnCards = new List<Card>();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Suit == bestSuit)
                {
                    returnCards.Add(cards[i]);
                }
            }

            return returnCards;
        }

        //Gets the probable collecting suit of the opponent
        private Suit GetPropableOpponentCollectingSuit()
        {
            //int array to hold the amounts
            int[] amounts = new int[4];

            //Go through the list
            for (int i = 0; i < OpponentCards.Count; i++)
            {
                //If the card is null, skip it
                if (OpponentCards[i] != null)
                {
                    //Add the amount
                    amounts[(int)OpponentCards[i].Suit] += OpponentCards[i].Value;
                }
            }

            int maxValue = 0;

            for (int i = 0; i < amounts.Length; i++)
            {
                if (amounts[i] > maxValue)
                {
                    maxValue = amounts[i];
                    TempSuit = (Suit)i;
                }
            }

            return TempSuit;
        }

        private int GetAmountOfCardsOfBestSuit()
        {
            int cardsOfBestSuit = 0;

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit == BestSuit)
                {
                    cardsOfBestSuit++;
                }
            }

            return cardsOfBestSuit++;
        }

        private Card GetWorstCardOfSuit(Suit suit)
        {
            int lowestValue = 12;
            Card lowestCard = null;

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit == suit && Hand[i].Value < lowestValue)
                {
                    lowestValue = Hand[i].Value;
                    lowestCard = Hand[i];
                }
            }

            return lowestCard;
        }

        void Prio()
        {
            int[] prio = new int[4];

            int[] cardsOnHand = new int[4];

            //Go through the list
            for (int i = 0; i < Hand.Count; i++)
            {
                //If the card is null, skip it
                if (OpponentCards[i] != null)
                {
                    //Add the amount
                    cardsOnHand[(int)OpponentCards[i].Suit] += Hand[i].Value;
                }
            }

            int maxValue = 0;
            bool[] hasBeenDone = new bool[4];

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < cardsOnHand.Length; i++)
                {
                    if (cardsOnHand[i] > maxValue && !hasBeenDone[j])
                    {
                        maxValue = cardsOnHand[i];
                        prio[j] = i;
                        hasBeenDone[j] = true;
                    }
                }

                int intToRemove = cardsOnHand.ToList().IndexOf(prio[0]);
                cardsOnHand[intToRemove] = 0;

            }
        }

    }

    class WORST_AI_IN_HUMAN_HISTORY : Player
    {

        public WORST_AI_IN_HUMAN_HISTORY()
        {
            Name = "Worst AI in human history";
        }

        public override bool Knacka(int round)
        {
            if (round == 2)
            {
                if (Game.Score(this) >= 18)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (round == 3)
            {
                if (Game.Score(this) >= 20)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (round >= 4)
            {
                if (Game.Score(this) >= 22)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            return false;
        }

        public override bool TaUppKort(Card card)
        {
            if (card == null) return false;

            if (card.Value == 11 || (card.Value == 10 && card.Suit == BestSuit))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public override Card KastaKort()
        {
            Card throwaway = null;

            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit != BestSuit)
                {
                    throwaway = Hand[i];
                }
            }
            if (throwaway == null)
            {
                Game.Score(this);
                Card worstcard = Hand.First();
                for (int i = 1; i < Hand.Count; i++)
                {
                    if (Hand[i].Value < worstcard.Value)
                    {
                        return worstcard;
                    }
                }
            }
            return throwaway;
        }

        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

        }
    }

    class FabianPlayer : Player //Denna spelare fungerar exakt som MyPlayer. Ändra gärna i denna för att göra tester.
    {
        private Card[] KnownOpponentCards = new Card[3];
        public FabianPlayer()
        {
            Name = "FabianPlayer";
        }

        public override bool Knacka(int round)
        {

            if (Game.Score(this) >= 24)
            {
                return true;
            }
            else if ((Game.Score(this) >= 20 && round == 3) || (Game.Score(this) >= 20 && round == 2))
            {
                return true;
            }
            else if ((Game.Score(this) >= 22 && round <= 8))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool TaUppKort(Card card)
        {
            int lowestCardInHandValue = 12;
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Value < card.Value)
                {
                    lowestCardInHandValue = card.Value;
                }
            }
            if (card.Value == 11 || (card.Value == 10 && card.Suit == BestSuit) || (card.Value >= lowestCardInHandValue && card.Suit == BestSuit))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override Card KastaKort()
        {
            Game.Score(this);
            Card worstCard = Hand.First();
            int numberOfAce = 0;
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Value == 11)
                {
                    numberOfAce++;
                }
            }
            bool[] aceAndDressed = new bool[3];
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit == BestSuit && Hand[i].Value == 11)
                {
                    aceAndDressed[0] = true;
                }
                else if (Hand[i].Suit == BestSuit && Hand[i].Value == 10)
                {
                    aceAndDressed[1] = true;
                }
                if (aceAndDressed[0] && aceAndDressed[1])
                {
                    aceAndDressed[2] = true;
                }
            }
            int howManyOfBestSuit = 0;
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Suit == BestSuit)
                {
                    howManyOfBestSuit++;
                }
            }
            for (int i = 1; i < Hand.Count; i++)
            {
                if (howManyOfBestSuit == 4 && worstCard.Value > Hand[i].Value)
                {
                    worstCard = Hand[i];
                }
                else if (howManyOfBestSuit == 3 && Hand[i].Suit != BestSuit)
                {
                    worstCard = Hand[i];
                }
                else if (aceAndDressed[2] && Hand[i].Suit != BestSuit)
                {
                    worstCard = Hand[i];
                }
                else if (numberOfAce == 2)
                {
                    if (Hand[i].Value != 11 && Hand[i].Value < worstCard.Value)
                    {
                        worstCard = Hand[i];
                    }
                }
                else if (Hand[i].Value < worstCard.Value && Hand[i].Suit != BestSuit && Hand[i].Value != 11)
                {
                    worstCard = Hand[i];
                }
            }
            return worstCard;

        }

        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

        }
        private void RegisterOpponentCards()
        {
            //KnownOpponentCards.Add(card)
        }
    }

}

