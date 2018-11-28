using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _31AI.AI
{
    abstract class Player
    {
        public string Name;
        // Dessa variabler får ej ändras

        public List<Card> Hand = new List<Card>();  // Lista med alla kort i handen. Bara de tre första platserna har kort i sig när rundan börjar, ett fjärde läggs till när man tar ett kort
        public Game Game;
        public Suit BestSuit; // Den färg med mest poäng. Uppdateras varje gång game.Score anropas
        public int Wongames;
        public int TrettiettWins;
        public int KnackWins;
        public int DefWins;
        public int KnackTotal;
        public int StoppedGames;
        public int StoppedRounds;
        public int PrintPosition;
        public bool lastTurn; // True om motståndaren har knackat, annars false.
        public Card OpponentsLatestCard; // Det senaste kortet motståndaren tog. Null om kortet drogs från högen.

        public abstract bool Knacka(int round);
        public abstract bool TaUppKort(Card card);
        public abstract Card KastaKort();
        public abstract void SpelSlut(bool wonTheGame);
    }

    class BasicPlayer : Player //Denna spelare fungerar exakt som MyPlayer. Ändra gärna i denna för att göra tester.
    {

        public BasicPlayer()
        {
            Name = "BasicPlayer";
        }

        public override bool Knacka(int round)
        {
            if (Game.Score(this) >= 30)
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
            if (card != null)
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
            return false;
        }

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

        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

        }
    }

    class test : Player //Denna spelare fungerar exakt som MyPlayer. Ändra gärna i denna för att göra tester.
    {

        public test()
        {
            Name = "MyPlayer";
        }

        public override bool Knacka(int round)
        {
            if (Game.Score(this) >= 30)
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

        public override void SpelSlut(bool wonTheGame)
        {
            if (wonTheGame)
            {
                Wongames++;
            }

        }
    }
}
