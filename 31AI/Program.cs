using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _31AI.AI;


namespace _31AI
{
    public enum Suit { Hjärter, Ruter, Spader, Klöver };

    class Program
    {

        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Game game = new Game();

            List<Player> players = new List<Player>();


            players.Add(new BasicPlayer());
            players.Add(new MyPlayer());
            players.Add(new TestAI());
            players.Add(new OtherAI());
            players.Add(new WORST_AI_IN_HUMAN_HISTORY());

            Console.WriteLine("Vilka två spelare skall mötas?");
            for (int i = 1; i <= players.Count; i++)
            {
                Console.WriteLine(i + ": {0}", players[i - 1].Name);
            }
            int p1 = int.Parse(Console.ReadLine());
            int p2 = int.Parse(Console.ReadLine());
            Player player1 = players[p1 - 1];
            Player player2 = players[p2 - 1];
            player1.Game = game;
            player1.PrintPosition = 0;
            player2.Game = game;
            player2.PrintPosition = 9;
            game.Player1 = player1;
            game.Player2 = player2;
            Console.WriteLine("Hur många spel skall spelas?");
            int numberOfGames = int.Parse(Console.ReadLine());
            Console.WriteLine("Skriva ut första spelet? (y/n)");
            string print = Console.ReadLine();
            Console.Clear();
            if (print == "y")
                game.Printlevel = 2;
            else
                game.Printlevel = 0;
            game.initialize(true);
            game.PlayAGame(true);
            Console.Clear();
            bool player1starts = true;

            for (int i = 1; i < numberOfGames; i++)
            {
                game.Printlevel = 0;
                player1starts = !player1starts;
                game.initialize(false);
                game.PlayAGame(player1starts);

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 3);
                Console.Write(player1.Name + ":");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.SetCursorPosition((player1.Wongames * 100 / numberOfGames) + 15, 3);
                Console.Write("█");
                Console.SetCursorPosition((player1.Wongames * 100 / numberOfGames) + 16, 3);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(player1.Wongames);

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 5);
                Console.Write(player2.Name + ":");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((player2.Wongames * 100 / numberOfGames) + 15, 5);
                Console.Write("█");
                Console.SetCursorPosition((player2.Wongames * 100 / numberOfGames) + 16, 5);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(player2.Wongames);

            }
            Console.SetCursorPosition(25, 7);
            Console.Write(player1.Name);
            Console.SetCursorPosition(45, 7);
            Console.WriteLine(player2.Name);
            Console.WriteLine("          Vunna spel:");
            Console.WriteLine("            Skillnad:");
            Console.WriteLine("Antal ronder i snitt:");
            Console.WriteLine("    Egna knackningar:");
            Console.WriteLine("         Andel vunna:");
            Console.WriteLine("  Knackpoäng i snitt:");
            Console.WriteLine("            Antal 31:");
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 8);
            Console.Write(player1.Wongames);
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 9);
            int diff = player1.Wongames - player2.Wongames;
            if (diff > 0)
            {
                Console.Write("+" + diff);
            }
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 10);
            double avgRounds1 = Math.Round((double)player1.StoppedRounds / (double)player1.StoppedGames, 2);
            Console.Write(avgRounds1);
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 11);
            Console.Write(player1.KnackWins + player2.DefWins);
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 12);
            double winPercent1 = Math.Round((double)player1.KnackWins * 100 / (player1.KnackWins + player2.DefWins), 1);
            Console.Write(winPercent1 + " %");
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 13);
            double knackAvg = Math.Round((double)player1.KnackTotal / (player1.KnackWins + player2.DefWins), 1);
            Console.Write(knackAvg);
            Console.SetCursorPosition(25 + player1.Name.Length / 2, 14);
            Console.Write(player1.TrettiettWins);

            Console.SetCursorPosition(45 + player2.Name.Length / 2, 8);
            Console.Write(player2.Wongames);
            Console.SetCursorPosition(45 + player2.Name.Length / 2, 9);
            int diff2 = player2.Wongames - player1.Wongames;
            if (diff2 > 0)
            {
                Console.Write("+" + diff2);
            }
            Console.SetCursorPosition(45 + player2.Name.Length / 2, 10);
            double avgRounds2 = Math.Round((double)player2.StoppedRounds / (double)player2.StoppedGames, 2);
            Console.Write(avgRounds2);

            Console.SetCursorPosition(45 + player2.Name.Length / 2, 11);
            Console.Write(player2.KnackWins + player1.DefWins);
            Console.SetCursorPosition(45 + player2.Name.Length / 2, 12);
            double winPercent2 = Math.Round((double)player2.KnackWins * 100 / (player2.KnackWins + player1.DefWins), 1);
            Console.Write(winPercent2 + " %");
            Console.SetCursorPosition(45 + player2.Name.Length / 2, 13);
            double knackAvg2 = Math.Round((double)player2.KnackTotal / (player2.KnackWins + player1.DefWins), 1);
            Console.Write(knackAvg2);
            Console.SetCursorPosition(45 + player2.Name.Length / 2, 14);
            Console.Write(player2.TrettiettWins);
            Console.ReadLine();
            Console.ReadLine();

        }

    }
}
