using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Deck
    {
        private Random rng = new Random();
        public IList<PlayingCard> Cards = new List<PlayingCard>();

        public Deck()
        {
            Build();
            Shuffle();
        }

        public void Build()
        {
            //  >>>>>[  Build the Deck. Eliminating Jokers for now.
            //          -----
            foreach (CardSuit Suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>())
                foreach (CardRank Rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
                    if (Rank != CardRank.Joker)
                        Cards.Add(new PlayingCard(Suit, Rank));
        }

        public void Shuffle()
        {
            //  >>>>>[  Shuffle
            //          -----
            for (int index = 51; index >= 0; index--)
            {
                int SwapCard = rng.Next(index + 1);
                PlayingCard SwapValue = Cards[SwapCard];
                Cards[SwapCard] = Cards[index];
                Cards[index] = SwapValue;
            }
        }

        public String Draw()
        {
            String returnValue = Cards.FirstOrDefault().ToString();
            Cards.Remove(Cards.FirstOrDefault());
            return (returnValue);
        }

        public Boolean Empty()
        {
            return (Cards.Count == 0);
        }
    }
}
