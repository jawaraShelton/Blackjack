using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{

    class BlackjackPlayer: IPlayer
    {

        #region IPlayer Implementation
        //  >>>>>[  Implement interface IPlayer
        //          - jds | 2019.01.25
        //          -----
        public Hand PlayerHand;
        Hand IPlayer.PlayerHand
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
            PlayerHand.Add(Card);
            this.Bust = (ValueOfHand() > 21);
        }

        public string ShowHand()
        {
            return PlayerHand.Show();
        }

        #endregion

        //  >>>>>[  Add features specific to the BlackJackPlayer
        //          - jds | 2019.01.25
        //          -----
        public int Cash { get; set; }
        public int Bet { get; set; }

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
            PlayerHand = new Hand();
            NewHand();
        }

        public BlackjackPlayer(String Name, int fundsAvailable)
        {
            PlayerHand = new Hand();

            PlayerName = Name;
            Cash = fundsAvailable;
        }

        #endregion

        public void Win()
        {
        }

        public Boolean DoubleDown()
        {
            if (Bet * 2 <= Cash)
                Bet = Bet * 2;

            return Bet * 2 <= Cash;
        }

        public void LoseWager()
        {
            Cash -= Bet;
        }

        public void WinWager()
        {
            if (HasBlackjack)
                Cash += (int)(Bet * 1.5);
            else
                Cash += Bet;
        }

        public void Push()
        {
            //  >>>>>[  Since the value of the bet is not subtracted from
            //          the available cash until after the hand is lost,
            //          there is nothing that needs to be done here.
            //          -----
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
            Surrendered = true;
        }

    }
}
