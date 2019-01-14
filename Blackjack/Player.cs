using System;

namespace Blackjack
{
    class Player
    {
        protected Hand hand = new Hand();

        private String playerName = "";
        public String PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
            }
        }

        private Boolean bust = false;
        public Boolean Bust
        {
            get
            {
                return bust;
            }
        }

        private Boolean standing = false;
        public Boolean Standing
        {
            get
            {
                return standing;
            }
        }

        private Boolean surrendered = false;
        public Boolean Surrendered
        {
            get
            {
                return surrendered;
            }
        }

        private Boolean canSurrender = true;
        public Boolean CanSurrender
        {
            get
            {
                return canSurrender;
            }
        }

        public Player()
        {
        }

        public Player(String Name)
        {
            playerName = Name;
        }

        public void Win()
        {
        }

        public void AddToHand(String Card)
        {
            hand.Add(Card);
            if (ValueOfHand() > 21)
                this.bust = true;
        }

        public int ValueOfHand()
        {
            return hand.Value();
        }

        public String ShowHand()
        {
            return hand.Show();
        }

        public void Stand()
        {
            standing = true;
        }

        public void NoSurrender()
        {
            canSurrender = false;
        }

        public void Surrender()
        {
            surrendered = true;
        }

        public void NewHand()
        {
            hand.Clear();
            bust = false;
            standing = false;
        }
    }
}
