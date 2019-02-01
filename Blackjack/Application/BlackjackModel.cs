using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Application
{
    class BlackjackModel : Model
    {
        private BlackjackPlayer Player;
        private BlackjackDealer Dealer;
        private BlackjackView View;

        private Dictionary<String, Boolean> Commands = new Dictionary<String, Boolean>();
        private List<String> FlavorText = new List<String>();
        private List<String> ResultText = new List<String>();

        public BlackjackModel(BlackjackDealer Dealer, BlackjackPlayer Player)
        {
            //  >>>>>[  For now keeping it simple: One player, one view. 
            //          - jds | 2019.01.30
            //          -----
            this.Dealer = Dealer;
            this.Player = Player;

            Dealer.NewHand();
            Player.NewHand();

            //  >>>>>[  Populate the command list. Initially, the only available
            //          command will be "Bet".
            //          -jds | 2019.01.31
            //          -----
            this.Commands.Add("bet", true);
            this.Commands.Add("hit", false);
            this.Commands.Add("stand", false);
            this.Commands.Add("double down", false);
            this.Commands.Add("surrender", false);
            this.Commands.Add("quit", true);
        }

        public void LinkView(BlackjackView View)
        {
            this.View = View;
        }

        public String AvailableCommands()
        {
            StringBuilder outStr = new StringBuilder();

            foreach (KeyValuePair<String, Boolean> d in Commands)
                if (d.Value)
                    outStr.Append(d.Key + " | ");

            return outStr.ToString().Substring(0, outStr.Length - 3).Trim();
        }

        public String GetPhase()
        {
            if (Commands["bet"])
                return "bet";
            else
                return "play";
        }

        public List<String> GetFlavorText()
        {
            return FlavorText;
        }

        public List<String> GetResultText()
        {
            return ResultText;
        }

        public String GetPlayerHand()
        {
            return Player.ShowHand();
        }

        public String GetDealerHand()
        {
            return Dealer.ShowHand();
        }

        public int GetWager()
        {
            return Player.Bet;
        }

        public BlackjackPlayer GetPlayer()
        {
            return Player;
        }

        public int GetCashAvailable()
        {
            return Player.Cash;
        }

        public void Bet(int amount)
        {
            if (amount <= Player.Cash)
            {
                Player.Bet = amount;

                List<string> keyList = new List<string>(Commands.Keys);
                foreach (string str in keyList)
                    Commands[str] = (!str.Equals("bet"));
            }

            Deal();
        }

        private void ResetCommandAvailability()
        {
            List<string> keyList = new List<string>(Commands.Keys);
            foreach (string str in keyList)
                Commands[str] = (str.Equals("bet") || str.Equals("quit"));
        }

        private void NoSurrender()
        {
            Player.NoSurrender();
            Commands["surrender"] = false;
        }

        public void Deal()
        {
            //  >>>>>[  Clear Player and Dealer's hand.
            //          -----
            Player.NewHand();
            Dealer.NewHand();

            //  >>>>>[  Shuffle the deck.
            //          -----
            Dealer.Shuffle();

            //  >>>>>[  Initial Deal
            //          -----
            for (int SubIndex = 0; SubIndex < 2; SubIndex++)
            {
                Player.AddToHand(Dealer.Deal());
                Dealer.AddToHand(Dealer.Deal());
            }

            View.ModelChanged();
        }

        public void Hit()
        {
            //  >>>>>[  Signal: Scrape cards against table (in handheld games); 
            //          tap the table with finger or wave hand toward body 
            //          (in games dealt face up).
            //          -----

            if(Commands["hit"])
            {
                NoSurrender();

                Player.AddToHand(Dealer.Deal());

                FlavorText.Clear();
                ResultText.Clear();

                FlavorText.Add("The Dealer slides you a card.");
                if (Player.Bust)
                {
                    ResultText.Add("And the Player goes bust...");
                    View.ModelChanged(true);

                    DealerGo();
                }
                else
                {
                    View.ModelChanged();
                }
            }
            else
            {
                FlavorText.Add("Command not available.");
                View.ModelChanged();
            }
        }

        public void Stand()
        {
            // >>>>>[   Signal: Slide cards under chips (in handheld games); 
            //          wave hand horizontally (in games dealt face up).
            //          -----
            if (Commands["stand"])
            {
                Player.Stand();
                Dealer.PlayHand();

                FlavorText.Clear();
                ResultText.Clear();

                FlavorText.Add("Player stands.");
                View.ModelChanged(true);

                DealerGo();
            }
            else
            {
                FlavorText.Add("Command not available.");
                View.ModelChanged();
            }
        }

        public void DoubleDown()
        {
            // >>>>>[   Signal: Place additional chips beside the original bet 
            //          outside the betting box, and point with one finger.
            //          -----
            NoSurrender();

            FlavorText.Clear();
            ResultText.Clear();

            if (Commands["double down"])
            {
                if (Player.DoubleDown())
                {
                    FlavorText.Add("You place the additional chips beside your original bet--outside the betting box.");
                    FlavorText.Add("Player's bet is now $" + Player.Bet.ToString());

                    Player.AddToHand(Dealer.Deal());
                    if (Player.Bust)
                    {
                        ResultText.Add("And the Player goes bust...");
                        View.ModelChanged(true);

                        DealerGo();
                    }
                    else
                    {
                        Player.Stand();
                        ResultText.Add("Player stands.");
                        View.ModelChanged(true);

                        DealerGo();
                    }
                }
                else
                {
                    FlavorText.Add("You do not have enough money for that.");
                    View.ModelChanged();
                }
            }
            else
            {
                FlavorText.Add("Command not available.");
                View.ModelChanged();
            }
}

        public void Split()
        {
            //  >>>>>[  Signal: Place additional chips next to the original bet 
            //          outside the betting box; point with two fingers spread 
            //          into a V formation.
            //          -----
            FlavorText.Clear();
            FlavorText.Add("Not Supported (yet). See list of available commands.");

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
            FlavorText.Clear();
            ResultText.Clear();

            if (Commands["surrender"])
            {
                if (Player.CanSurrender)
                {
                    Player.Surrender();

                    FlavorText.Add(Player.PlayerName + " surrenders the hand .");
                    FlavorText.Add("Bet is now $" + Player.Bet.ToString());
                    DealerGo();
                }
                else
                {
                    FlavorText.Add("That option is only available as the first decision of your hand.");
                }

                View.ModelChanged();
            }
            else
            {
                FlavorText.Add("Command not available.");
                View.ModelChanged();
            }
        }

        private void SetupNewHand()
        {
            Dealer.NewHand();
            Player.NewHand();

            Dealer.ResetReveal();

            Player.CanSurrender = true;
            Player.Bust = false;
            Player.Standing = false;
            Player.Surrendered = false;
            Player.Bet = 0;

            FlavorText.Clear();
            ResultText.Clear();

            ResetCommandAvailability();

            View.ModelChanged();
        }

        private void DealerGo()
        {
            //  >>>>>[  Score the hand, and distribute payouts.
            //          -----
            FlavorText.Clear();
            ResultText.Clear();

            if (!Player.Bust)
            {
                FlavorText.Add("Dealer Plays...");
                Dealer.PlayHand();

                if (Dealer.PlayerHand.Value() > 21)
                {
                    ResultText.Add("Dealer Busts!");
                    ResultText.Add(Player.PlayerName + " WINS!");
                    Player.WinWager();
                }
                else
                {
                    if (Player.ValueOfHand == Dealer.PlayerHand.Value())
                    {
                        ResultText.Add("Push.");
                        Player.Push();
                    }

                    if (Player.ValueOfHand < Dealer.PlayerHand.Value())
                    {
                        ResultText.Add(Player.PlayerName + " loses.");
                        Player.LoseWager();
                    }

                    if (Player.ValueOfHand > Dealer.PlayerHand.Value())
                    {
                        ResultText.Add(Player.PlayerName + " WINS!");
                        Player.WinWager();
                    }
                }
            }
            else
            {
                ResultText.Add(Player.PlayerName + " loses.");
                Player.LoseWager();
            }

            View.ModelChanged(true);
            SetupNewHand();
        }
    }
}
