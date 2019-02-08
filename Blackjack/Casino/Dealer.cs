using System;
using System.Collections.Generic;

namespace Blackjack
{
    abstract class Dealer: IPlayer
    {
        #region IPlayer Implementation
        //  >>>>>[  Implement interface IPlayer
        //          - jds | 2019.01.25
        //          -----

        public IHand PlayerHand
        {
            get
            {
                return PlayerHand;
            }
            set
            {
                PlayerHand = value;
            }
        }

        public String PlayerName;
        string IPlayer.PlayerName
        {
            get
            {
                return PlayerName;
            }
            set
            {
                PlayerName = value;
            }
        }

        public void NewHand()
        {
            if(PlayerHand == null)
                PlayerHand = new Hand();

            PlayerHand.Clear();
        }

        public void AddToHand(string Card)
        {
            PlayerHand.Add(Card);
        }

        public virtual string ShowHand()
        {
            return PlayerHand.Show();
        }

        #endregion

        protected Shoe shoe;
        protected List<IPlayer> players;

        public Dealer()
        {
            players = new List<IPlayer>();
            Shuffle();
        }

        public abstract void Shuffle(int numberOfDecks = 1);
        public abstract String Deal();

        public abstract String PlayHand();

        public void AddPlayer(IPlayer player)
        {
            players.Add(player);
        }
    }
}
