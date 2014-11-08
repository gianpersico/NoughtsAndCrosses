using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoughtsAndCrosses.Domain;

namespace NoughtsAndCrosses.Ui.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("*******************************");
            WriteLine("WELCOME TO NOUGHTS AND CROSSES!");
            WriteLine("*******************************\n");

            WriteLine("Press 'N' to start a new game...");
            var selectedOption = ReadLine().ToLower();

            if (selectedOption == "n")
            {
                var game = Game.StartNew();
                WriteLine(string.Format("Game {0} started...", game.Id));
                var players = game.Players.ToList();
                var player1 = players[0];
                var player2 = players[1];

                while (game.IsInProgress())
                {
                    WriteLine("Player 1 please enter x and y coordinates to take a turn in a the following format: 1 1...");
                    var playerOneMove = ReadLine();
                    game.TakeTurn(player1, new BoardSpace(int.Parse(playerOneMove.Split(' ')[0]), int.Parse(playerOneMove.Split(' ')[1])));
                                        
                    if(GameHasWinner(game)) break;

                    WriteLine("Player 2 please enter x and y coordinates to take a turn in a the following format: 1 1...");
                    var playerTwoMove = ReadLine();
                    game.TakeTurn(player2, new BoardSpace(int.Parse(playerTwoMove.Split(' ')[0]), int.Parse(playerTwoMove.Split(' ')[1])));

                    if (GameHasWinner(game)) break;

                    
                    
                }

            }
            





            System.Console.ReadKey();
        }

        private static bool GameHasWinner(Game game)
        {
            if (game.Winner != null)
            {
                // Write something for the winner...
                WriteLine("WINNER IS " + game.Winner.Name);
                return true;
            }
            return false;
        }

        private static void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        private static string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}
