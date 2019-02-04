using System;
using Blackjack.Blackjack;

namespace Blackjack
{

    class BlackjackPlayer: IPlayer
    {

        #region IPlayer Implementation
        //  >>>>>[  Implement interface IPlayer
        //          - jds | 2019.01.25
        //          -----
        public BlackjackHand PlayerHand = new BlackjackHand();
        Hand IPlayer.PlayerHand
        {
            get
            {
                return PlayerHand;
            }
            set
            {
                PlayerHand = (BlackjackHand) value;
            }
        }

        public String PlayerName;
        string IPlayer.PlayerName
        {
            get
            {
                return PlayerName;
            }
            set
            {
                PlayerName = value;
            }
        }

        public void NewHand()
        { 
            PlayerHand.Clear();
            Bust = false;
            Standing = false;
        }

        public void AddToHand(string Card)
        {
            PlayerHand.Add(Card);
            this.Bust = (ValueOfHand > 21);
        }

        public string ShowHand()
        {
            return PlayerHand.ToString();
        }

        #endregion

        //  >>>>>[  Add features specific to the BlackJackPlayer
        //          - jds | 2019.01.25
        //          -----
        public decimal Cash { get; set; }
        public decimal Bet { get; set; }

        public Boolean Bust { get; set; }
        public Boolean Standing { get; set; }
        public Boolean CanSurrender { get; set; }
        public Boolean Surrendered { get; set; }

        public Boolean HasBlackjack
        {
            get
            {
                return PlayerHand.IsBlackJack();
            }
        }

        public int ValueOfHand
        {
            get
            {
                return PlayerHand.Value();
            }
        }

        #region BlackjackPlayer Constructors

        public BlackjackPlayer(String Name = "Dealer")
        {
            PlayerHand = new BlackjackHand();
            NewHand();
        }

        public BlackjackPlayer(String Name, decimal fundsAvailable)
        {
            PlayerHand = new BlackjackHand();

            PlayerName = Name;
            Cash = fundsAvailable;
        }

        #endregion

        public void Win()
        {
        }

        public bool decimalDown()
        {
            decimal prevBet = Bet;
            if (Bet <= Cash)
            {
                Cash -= Bet;
                Bet = Bet * 2;
            }

            return prevBet <= Cash;
        }

        public void LoseWager()
        {
            //  >>>>>[  Since the bet has already been subtracted from the
            //          available cash there is nothing to be done here.
            //          -----
        }

        public void Win(decimal Amount)
        {
                Cash += Amount;
        }

        public void Push()
        {
            //  >>>>>[  Return the bet to the player's available cash.
            //          -----
            Cash += Bet;
        }

        public void Stand()
        {
            Standing = true;
        }

        public void NoSurrender()
        {
            CanSurrender = false;
        }

        public void Surrender()
        {
            Bet = Bet / 2;
            Cash += Bet / 2;
            Surrendered = true;
        }

    }
}
