using System;
using System.Collections.Generic;


namespace Blackjack
{
    class Shoe: IShoe
    {
        private int currentDeck;
        private IList<Deck> shoe;
        private Random rng;

        public Shoe()
        {
            shoe = new List<Deck>();
            rng = new Random();
        }

        public Shoe(int NumberOfDecks)
        {
            shoe = new List<Deck>();
            rng = new Random();

            for (int i = 0; i < NumberOfDecks; i++)
                this.Add(new Deck());
        }

        public Shoe(int NumberOfDecks, List<PlayingCard> CardsToExclude)
        {
            shoe = new List<Deck>();
            rng = new Random();

            for (int i = 0; i < NumberOfDecks; i++)
                this.Add(new Deck(CardsToExclude));
        }

        public void Add(IDeck deck)
        {
            shoe.Add((Deck) deck);
        }

        public void Add(Deck deck)
        {
            shoe.Add(deck);
        }

        public String Draw()
        {
            if (!this.Empty())
            {
                foreach (Deck d in shoe)
                {
                    d.Build();
                    d.Shuffle();
                }
            }

            do
            {
                currentDeck = rng.Next(shoe.Count);
            }
            while (shoe[currentDeck].Empty());

            return(shoe[currentDeck % shoe.Count].Draw());
        }

        public Boolean Empty()
        {
            Boolean retval = true;
            foreach(Deck d in shoe)
                retval = retval & d.Empty();

            return retval;
        }
    }
}
