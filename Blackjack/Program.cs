using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayingCard c1 = new PlayingCard(CardSuit.Diamonds, CardRank.Ace);
            PlayingCard c2 = new PlayingCard(CardSuit.Spades, CardRank.Ace);
            if (c1 == c2)
                Console.WriteLine("The cards match.");

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

                //  >>>>>[  Shuffle the deck
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
                        Boolean done = false;
                        while (!playerInGame.Standing && !playerInGame.Bust && !done)
                        {
                            Console.Write("Ready, {0}: ", playerInGame.PlayerName);
                            String userInput = Console.ReadLine().ToLower();
                            switch (userInput)
                            {
                                case "hit":
                                    //  >>>>>[  Signal: Scrape cards against table (in handheld games); 
                                    //          tap the table with finger or wave hand toward body 
                                    //          (in games dealt face up).
                                    //          -----
                                    playerInGame.NoSurrender();
                                    playerInGame.AddToHand(dealer.Deal());
                                    Console.WriteLine("{0}'s Hand: {1}", playerInGame.PlayerName, playerInGame.ShowHand());
                                    if (playerInGame.Bust)
                                    {
                                        Console.WriteLine("And the Player goes bust...");
                                        done = true;
                                    }
                                    break;
                                case "stand":
                                    // >>>>>[   Signal: Slide cards under chips (in handheld games); 
                                    //          wave hand horizontally (in games dealt face up).
                                    //          -----
                                    playerInGame.NoSurrender();
                                    playerInGame.Stand();
                                    Console.WriteLine("Player stands.");
                                    done = true;
                                    break;
                                case "double down":
                                    // >>>>>[   Signal: Place additional chips beside the original bet 
                                    //          outside the betting box, and point with one finger.
                                    //          -----
                                    playerInGame.NoSurrender();
                                    break;
                                case "split":
                                    //  >>>>>[  Signal: Place additional chips next to the original bet 
                                    //          outside the betting box; point with two fingers spread 
                                    //          into a V formation.
                                    //          -----
                                    playerInGame.NoSurrender();
                                    break;
                                case "surrender":
                                    //  >>>>>[  NO SIGNAL! The request to surrender is made verbally, 
                                    //          there being no standard hand signal.
                                    //
                                    //          When the player surrenders they give up the hand but 
                                    //          lose only half their bet as a result.
                                    //
                                    //          NOTE: Only available as first decision of hand.
                                    //          -----
                                    if (playerInGame.CanSurrender)
                                    {
                                        playerInGame.Surrender();
                                        Console.WriteLine("{0} surrenders the hand.", playerInGame.PlayerName);
                                        done = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("That options is only available as the first decision of your hand.");
                                    }
                                    break;
                                default:
                                    //  >>>>>[  Nothing to be done here. Continue looping.
                                    //          -----
                                    break;
                            }
                        }
                    }

                    //  >>>>>[  Dealer plays its hand here.
                    //          -----

                    //  >>>>>[  Complete hand to end the loop.
                    //          -----
                    HandCompleted = true;
                }

                Console.Write("Quit Playing? (Y/N): ");
                switch (Console.Read().ToString().ToUpper())
                {
                    case "Y":
                        PlayerQuit = true;
                        break;
                    default:
                        //  >>>>>[  Nothing to do here.
                        //          -----
                        break;
                }
            }
        }
    }
}
