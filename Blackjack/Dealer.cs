using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer: Player
    {
        private IList<Deck> shoe = new List<Deck>();
        public int CurrentDeck = 0;

        public Dealer()
        {
            Score = 0;
            Reshuffle();
        }

        public void Reshuffle()
        {
            shoe.Clear();
            for (int Index = 0; Index < Properties.Settings.Default.NumberOfDecks; Index++)
                shoe.Add(new Deck());
        }

        public String Deal()
        {
            String returnValue = shoe[CurrentDeck % shoe.Count].Draw();
            CurrentDeck++;

            return (returnValue);
        }

        public new String ShowHand()
        {
            String returnValue = hand.Show();

            returnValue = "??" + returnValue.Substring(returnValue.IndexOf(' ')); 
            return (returnValue);
        }
    }
}
