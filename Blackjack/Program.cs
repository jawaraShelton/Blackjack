using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLoop();
        }

        static void GameLoop()
        {
            List<Player> players = new List<Player>();

            Dealer dealer = new Dealer();

            int PlayerNumber = 1;
            for (int Index = 0; Index < Properties.Settings.Default.NumberOfPlayers; Index++)
                players.Add(new Player("Player " + PlayerNumber.ToString()));

            Boolean PlayerQuit = false;

            while (!PlayerQuit)
            {
                //  >>>>>[  Clear everyone's hand
                //          -----
                foreach (Player playerInGame in players)
                    playerInGame.NewHand();

                dealer.NewHand();

                //  >>>>>[  
                //          -----
                dealer.Reshuffle();

                //  >>>>>[  Initial Deal
                //          -----
                for (int SubIndex = 0; SubIndex < 2; SubIndex++)
                {
                    foreach (Player playerInGame in players)
                        playerInGame.AddToHand(dealer.Deal());

                    dealer.AddToHand(dealer.Deal());
                }

                //  >>>>>[  Gameplay
                //          -----
                Boolean HandCompleted = false;
                while (!HandCompleted)
                {
                    Console.WriteLine("Dealer's Hand  : {0}", dealer.ShowHand());
                    foreach (Player playerInGame in players)
                        Console.WriteLine("{0}'s Hand: {1}", playerInGame.PlayerName, playerInGame.ShowHand());
                    Console.WriteLine();

                    foreach (Player playerInGame in players)
                    {
                        Console.Write("Ready, {0}: ", playerInGame.PlayerName);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
