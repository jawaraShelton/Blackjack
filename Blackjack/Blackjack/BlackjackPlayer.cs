using System;
using System.Collections.Generic;
using Blackjack.Blackjack;

namespace Blackjack
{

    class BlackjackPlayer: IPlayer
    {

        #region IPlayer Implementation
        //  >>>>>[  Implement interface IPlayer
        //          - jds | 2019.01.25
        //          -----

        public List<IHand> PlayerHand
        {
            get
            {
                return PlayerHand;
            }
            set
            {
                PlayerHand = value;
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
            PlayerHand[0].Add(Card);
            PlayerHand[0].Bust = (PlayerHand[0].Value() > 21);
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

        public Boolean CanSurrender { get; set; }
        public Boolean Surrendered { get; set; }

        public int ValueOfHand
        {
            get
            {
                return PlayerHand[0].Value();
            }
        }

        #region BlackjackPlayer Constructors

        public BlackjackPlayer(IHand PlayerHand, decimal fundsAvailable, String Name = "Dealer")
        {
            this.PlayerHand.Add(PlayerHand);

            PlayerName = Name;
            Cash = fundsAvailable;
        }

        #endregion

        public void Win()
        {
        }

        public bool DoubleDown()
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
            PlayerHand[0].Standing = true;
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
