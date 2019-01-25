using System;
using System.Collections.Generic;

namespace Blackjack
{
    abstract class Dealer
    {
        protected Shoe shoe;
        protected List<IPlayer> players;

        public Dealer()
        {
            players = new List<IPlayer>();
            Shuffle();
        }

        public abstract void Shuffle(int numberOfDecks = 1);
        public abstract String Deal();

        public abstract String ShowHand();
        public abstract String PlayHand();

        public abstract void Go();

        public void AddPlayer(IPlayer player)
        {
            players.Add(player);
        }
    }
}
