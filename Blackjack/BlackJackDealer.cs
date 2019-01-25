using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class BlackjackDealer: CasinoDealer
    {
        protected BlackjackPlayer me;
        // protected new List<BlackjackPlayer> players;

        public BlackjackDealer()
        {
            //players = new List<BlackjackPlayer>();
            me = new BlackjackPlayer();
            Shuffle();
        }

        //public void AddPlayer(BlackjackPlayer player)
        //{
        //    players.Add(player);
        //}

        public override String Deal()
        {
            return (shoe.Draw());
        }

        public override String ShowHand()
        {
            String returnValue = me.PlayerHand.Show();

            returnValue = "??" + returnValue.Substring(returnValue.IndexOf(' '));
            return (returnValue);
        }

        public override String PlayHand()
        {
            while (me.PlayerHand.Value() <= 17)
                me.PlayerHand.Add(Deal());

            return me.PlayerHand.Show() + "\n" + (me.PlayerHand.Value() > 21 ? "Dealer Busts." : "");
        }

        public override void Go()
        {
            //  >>>>>[  Game logic developed using rules found at 
            //          https://www.pagat.com/banking/blackjack.html
            //
            //          jds | 2019.01.23
            //          -----

            Boolean PlayerQuit = false;

            while (!PlayerQuit)
            {
                //  >>>>>[  Clear everyone's hand
                //          -----
                foreach (BlackjackPlayer playerInGame in players)
                    playerInGame.NewHand();

                //  >>>>>[  Clear the dealer's hand
                //          -----
                me.NewHand();

                //  >>>>>[  Shuffle the deck
                //          -----
                Shuffle();

                //  >>>>>[  Initial Deal
                //          -----
                for (int SubIndex = 0; SubIndex < 2; SubIndex++)
                {
                    foreach (BlackjackPlayer playerInGame in players)
                        playerInGame.AddToHand(Deal());

                    me.AddToHand(Deal());
                }

                //  >>>>>[  Gameplay
                //          -----
                Boolean HandCompleted = false;
                while (!HandCompleted)
                {
                    Console.WriteLine("Dealer's Hand  : {0}", ShowHand());

                    foreach (BlackjackPlayer playerInGame in players)
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
                                    playerInGame.AddToHand(Deal());

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

                                    if (playerInGame.DoubleDown())
                                    {
                                        Console.WriteLine("You place the additional chips beside your original bet--outside the betting box.");
                                        Console.WriteLine("Player's bet is now ${0}", playerInGame.Bet);
                                        playerInGame.AddToHand(Deal());
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
                                        Console.WriteLine("{0} surrenders the hand .", playerInGame.PlayerName);
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
                    Console.WriteLine("Dealer's hand is: {0}", PlayHand());

                    //  >>>>>[  Score the hand, and distribute payouts.
                    //          -----

                    foreach (BlackjackPlayer playerInGame in players)
                    {
                        if (!playerInGame.Bust)
                        {
                            if (me.Bust)
                            {
                                playerInGame.WinWager();
                            }
                            else
                            {
                                if (playerInGame.ValueOfHand() == me.ValueOfHand())
                                {
                                    Console.WriteLine("{0} is a push.", playerInGame.PlayerName);
                                    playerInGame.Push();
                                }

                                if (playerInGame.ValueOfHand() < me.ValueOfHand())
                                {
                                    Console.WriteLine("{0} loses the wager.", playerInGame.PlayerName);
                                    playerInGame.LoseWager();
                                }

                                if (playerInGame.ValueOfHand() > me.ValueOfHand())
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
