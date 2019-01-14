using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Deck
    {
        private List<String> Values = new List<String> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        private List<char> Suits = new List<char> { 'H', 'C', 'S', 'D' };
        private Random rng = new Random();

        public IList<String> Cards = new List<String>();

        public Deck()
        {
            Build();
            Shuffle();
        }

        public void Build()
        {
            //  >>>>>[  Build the Deck
            //          -----
            foreach (char CardSuit in Suits)
                foreach (String Value in Values)
                {
                    Cards.Add(Value + CardSuit.ToString());
                }
        }

        public void Shuffle()
        {
            //  >>>>>[  Shuffle
            //          -----
            for (int index = 51; index >= 0; index--)
            {
                int SwapCard = rng.Next(index + 1);
                String SwapValue = Cards[SwapCard];
                Cards[SwapCard] = Cards[index];
                Cards[index] = SwapValue;
            }
        }

        public String Draw()
        {
            String returnValue = Cards.FirstOrDefault();
            Cards.Remove(returnValue);
            return (returnValue);
        }

        public Boolean Empty()
        {
            return (Cards.Count == 0);
        }
    }
}
