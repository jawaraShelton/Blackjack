using System;

namespace Blackjack
{
    class Player
    {
        protected Hand hand = new Hand();
        protected int Score;

        public String PlayerName = "";
        public Boolean Bust = false;
        public Boolean Standing = false;

        public Player()
        {
            Score = 0;
        }

        public Player(String Name)
        {
            PlayerName = Name;
            Score = 0;
        }

        public void Win()
        {
            Score++;
        }

        public void AddToHand(String Card)
        {
            hand.Add(Card);
            if (ValueOfHand() > 21)
                Bust = true;
        }

        public int ValueOfHand()
        {
            return hand.Value();
        }

        public String ShowHand()
        {
            return hand.Show();
        }

        public void NewHand()
        {
            hand.Clear();
            Bust = false;
            Standing = false;
        }
    }
}
