using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Application
{
    class BlackjackModel: Model
    {
        private BlackjackPlayer Player;
        private BlackjackDealer Dealer;
        private BlackjackView View;

        public BlackjackModel(BlackjackDealer Dealer, BlackjackPlayer Player, BlackjackView View)
        {
            //  >>>>>[  For now keeping it simple: One player, one view. 
            //          - jds | 2019.01.30
            //          -----
            this.Dealer = Dealer;
            this.Player = Player;
            this.View = View;
        }

        public BlackjackPlayer GetPlayer()
        {
            return Player;
        }

        public void Hit()
        {
            //  >>>>>[  Signal: Scrape cards against table (in handheld games); 
            //          tap the table with finger or wave hand toward body 
            //          (in games dealt face up).
            //          -----
            Player.NoSurrender();
            Player.AddToHand(Dealer.Deal());

            Console.WriteLine("The Dealer slides you a card.");
            Console.WriteLine("{0}'s Hand: {1}", Player.PlayerName, Player.ShowHand());
            if (Player.Bust)
            {
                Console.WriteLine("And the Player goes bust...");
            }

            View.ModelChanged();
        }

        public void Stand()
        { 
            // >>>>>[   Signal: Slide cards under chips (in handheld games); 
            //          wave hand horizontally (in games dealt face up).
            //          -----
            Player.NoSurrender();
            Player.Stand();
            Console.WriteLine("Player stands.");

            View.ModelChanged();
        }

        public void DoubleDown()
        {
            // >>>>>[   Signal: Place additional chips beside the original bet 
            //          outside the betting box, and point with one finger.
            //          -----
            Player.NoSurrender();

            if (Player.DoubleDown())
            {
                Console.WriteLine("You place the additional chips beside your original bet--outside the betting box.");
                Console.WriteLine("Player's bet is now ${0}", Player.Bet);
                Player.AddToHand(Dealer.Deal());
                Console.WriteLine("{0}'s Hand: {1}", Player.PlayerName, Player.ShowHand());
                if (Player.Bust)
                {
                    Console.WriteLine("And the Player goes bust...");
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

            View.ModelChanged();
        }

        public void Split()
        {
            //  >>>>>[  Signal: Place additional chips next to the original bet 
            //          outside the betting box; point with two fingers spread 
            //          into a V formation.
            //          -----
            Console.WriteLine("Not Supported (yet). See list of available commands.");

            View.ModelChanged();
        }

        public void Surrender()
        {
            //  >>>>>[  NO SIGNAL! The request to surrender is made verbally, 
            //          there being no standard hand signal.
            //
            //          When the player surrenders they give up the hand but 
            //          lose only half their bet as a result.
            //
            //          NOTE: Only available as first decision of hand.
            //          -----
            if (Player.CanSurrender)
            {
                Player.Surrender();
                Console.WriteLine("{0} surrenders the hand .", Player.PlayerName);
                Console.WriteLine("Player's bet is now ${0}", Player.Bet);
            }
            else
            {
                Console.WriteLine("That option is only available as the first decision of your hand.");
            }

            View.ModelChanged();
        }
    }
}
