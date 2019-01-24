using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Blackjack
{
    class Program
    {
        //  >>>>>[  Developed using rules listed on https://www.pagat.com/banking/blackjack.html
        //          -----

        static void Main(string[] args)
        {
            GameLoop();
        }

        static void GameLoop()
        {
            List<Player> players = new List<Player>();
            Random rng = new Random();
            Dealer dealer = new BlackjackDealer();

            int PlayerNumber = 1;
            for (int Index = 0; Index < Properties.Settings.Default.NumberOfPlayers; Index++)
                players.Add(new Player("Player " + PlayerNumber.ToString(), 500));

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
                dealer.Shuffle();

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
                    {
                        Boolean done = false;

                        //  >>>>>[  Display the player's hand and available cash. 
                        //          Find out how much they're betting.
                        //          -----
                        Console.WriteLine("{0}'s Hand: {1}", playerInGame.PlayerName, playerInGame.ShowHand());
                        Console.WriteLine("Funds Available: {0}", playerInGame.Cash);
                        Console.Write("Bet: ");

                        playerInGame.Bet = Convert.ToInt32(Console.ReadLine());


                        while (!playerInGame.Standing && !playerInGame.Bust && !done)
                        {
                            Console.WriteLine("Available Commands: hit | stand | double down {0}", playerInGame.CanSurrender ? " | surrender" : "");
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

                                    Console.WriteLine("The Dealer slides you a card.");
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

                                    if(playerInGame.DoubleDown())
                                    {
                                        Console.WriteLine("You place the additional chips beside your original bet--outside the betting box.");
                                        Console.WriteLine("Player's bet is now ${0}", playerInGame.Bet);
                                        playerInGame.AddToHand(dealer.Deal());
                                        Console.WriteLine("{0}'s Hand: {1}", playerInGame.PlayerName, playerInGame.ShowHand());
                                        if (playerInGame.Bust)
                                        {
                                            Console.WriteLine("And the Player goes bust...");
                                            done = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Player stands.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You do not have enough money for that.");
                                    }
                                    break;
                                case "split":
                                    //  >>>>>[  Signal: Place additional chips next to the original bet 
                                    //          outside the betting box; point with two fingers spread 
                                    //          into a V formation.
                                    //          -----
                                    //  playerInGame.NoSurrender();
                                    Console.WriteLine("Not Supported (yet). See list of available commands.");
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
                                        Console.WriteLine("Player's bet is now ${0}", playerInGame.Bet);
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
                                    Console.WriteLine("Command not valid. Retry.");
                                    break;
                            }
                        }
                    }

                    //  >>>>>[  Dealer plays its hand here.
                    //          -----
                    Console.WriteLine("Dealer's hand is: {0}", dealer.PlayHand());

                    //  >>>>>[  Score the hand, and distribute payouts.
                    //          -----

                    foreach (Player playerInGame in players)
                    {
                        if (!playerInGame.Bust)
                        {
                            if (dealer.Bust)
                            {
                                playerInGame.WinWager();
                            }
                            else
                            {
                                if (playerInGame.ValueOfHand() == dealer.ValueOfHand())
                                {
                                    Console.WriteLine("{0} is a push.", playerInGame.PlayerName);
                                    playerInGame.Push();
                                }

                                if (playerInGame.ValueOfHand() < dealer.ValueOfHand())
                                {
                                    Console.WriteLine("{0} loses the wager.", playerInGame.PlayerName);
                                    playerInGame.LoseWager();
                                }

                                if (playerInGame.ValueOfHand() > dealer.ValueOfHand())
                                {
                                    Console.WriteLine("{0} WINS!", playerInGame.PlayerName);
                                    playerInGame.WinWager();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("{0} loses the wager.", playerInGame.PlayerName);
                            playerInGame.LoseWager();
                        }
                    }

                    //  >>>>>[  Complete hand to end the loop.
                    //          -----
                    HandCompleted = true;
                }

                Console.Write("Quit Playing? (Y/N): ");
                switch (Console.ReadLine().ToString().ToUpper())
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
