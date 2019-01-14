using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer: Player
    {
        Shoe shoe;

        public Dealer()
        {
            Reshuffle();
        }

        public void Reshuffle()
        {
            shoe = new Shoe(Properties.Settings.Default.NumberOfDecks);
        }

        public String Deal()
        {
            return (shoe.Draw());
        }

        public new String ShowHand()
        {
            String returnValue = hand.Show();

            returnValue = "??" + returnValue.Substring(returnValue.IndexOf(' ')); 
            return (returnValue);
        }
    }
}
