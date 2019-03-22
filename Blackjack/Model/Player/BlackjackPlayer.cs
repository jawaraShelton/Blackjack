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
        public List<BlackjackHand> playerHand;
        public List<BlackjackHand> PlayerHand
        {
            get
            {
                return playerHand;
            }
            set
            {
                playerHand = value;
            }
        }

        public String PlayerName;
        string IPlayer.PlayerName
        {
            get
            {
                return this.PlayerName;
            }
            set
            {
                this.PlayerName = value;
            }
        }

        public void NewHand()
        { 
            if(PlayerHand!=null)
                playerHand.Clear();
            ptrCur = 0;
        }

        public void AddToHand(string Card)
        {
            if (playerHand.Count == 0)
                playerHand.Add(new BlackjackHand());

            playerHand[ptrCur].Add(Card);
        }

        public string ShowHand()
        {
            return (playerHand.Count > 0 ? playerHand[ptrCur].ToString() : "EMPTY");
        }

        #endregion

        //  >>>>>[  Add features specific to the BlackJackPlayer
        //          - jds | 2019.01.25
        //          -----
        public decimal Cash { get; set; }
        public decimal Bet { get; set; }

        public Boolean CanSurrender { get; set; }
        public Boolean Surrendered { get; set; }

        protected Int16 ptrCur = 0;

        public int ValueOfHand
        {
            get
            {
                return playerHand[ptrCur].Value();
            }
        }

        #region BlackjackPlayer Constructors

        public BlackjackPlayer(BlackjackHand Hand, decimal fundsAvailable, String Name = "Dealer")
        {
            playerHand = new List<BlackjackHand>();
            playerHand.Add(Hand);

            PlayerName = Name;
            Cash = fundsAvailable;
        }

        #endregion

        public void Win()
        {
        }

        public BlackjackHand CurrentHand()
        {
            return playerHand[ptrCur];
        }

        public bool DoubleDown()
        {
            decimal prevBet = playerHand[ptrCur].Wager;
            if (prevBet <= Cash)
            {
                Cash -= prevBet;
                playerHand[ptrCur].Wager = prevBet * 2;
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
            Cash += playerHand[ptrCur].Wager;
        }

        public void Stand()
        {
            playerHand[ptrCur].Standing = true;
        }

        public void NoSurrender()
        {
            CanSurrender = false;
        }

        public void Surrender()
        {
            playerHand[ptrCur].Wager = playerHand[ptrCur].Wager / 2;
            Cash += playerHand[ptrCur].Wager / 2;
            Surrendered = true;
        }

        public Boolean Split(Decimal Wager)
        {
            Boolean retval = false;

            if (Wager < Cash)
            {
                playerHand.Add(new BlackjackHand(playerHand[ptrCur].SplitCard, Wager));
                playerHand[ptrCur].Cards.Remove(playerHand[ptrCur].SplitCard);

                Cash -= Wager;
                retval = true;
            }

            return (retval);
        }
    }
}
