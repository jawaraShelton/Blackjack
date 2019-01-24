using System;
using System.Collections.Generic;

namespace Blackjack
{
    abstract class Dealer : Player
    {
        protected Shoe shoe;
        protected List<Player> players;

        public Dealer()
        {
            players = new List<Player>();
            Shuffle();
        }

        public abstract void Shuffle(int numberOfDecks = 1);
        public abstract String Deal();

        public abstract new String ShowHand();
        public abstract String PlayHand();

        public abstract void Go();

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
    }
}
