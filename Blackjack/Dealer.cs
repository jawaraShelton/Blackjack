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
            //  >>>>>[  This is not a good way to approach this since the
            //          class is ultimately intended to be more generlized 
            //          to other casino card games, but will do for now.
            //          - jds 2019.01.14 
            //          -----
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

        public String PlayHand()
        {
            while (hand.Value() <= 17)
                hand.Add(Deal());

            return hand.Show() + "\n" + (hand.Value()>21 ? "Dealer Busts." : "");
        }
    }
}
