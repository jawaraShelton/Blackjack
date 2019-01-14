using System;
using System.Collections.Generic;

namespace Blackjack
{
    enum CardSuit
    {
        Diamonds = 0,
        Clubs = 1,
        Hearts = 2,
        Spades = 3
    }

    enum CardRank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Joker = 14
    }

    class PlayingCard
    {
        public CardSuit Suit { get; }
        public CardRank Rank { get; }

        public PlayingCard()
        {

        }

        public PlayingCard(CardSuit suit, CardRank rank)
        {
            try
            { 
            if (!Enum.IsDefined(typeof(CardSuit), suit))
                throw new ArgumentOutOfRangeException("Attempted to instantiate PlayingCard object with invalid CardSuit.");

            if (!Enum.IsDefined(typeof(CardRank), rank))
                throw new ArgumentOutOfRangeException("Attempted to instantiate PlayingCard object with invalid CardRank.");

            Suit = suit;
            Rank = rank;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                Environment.Exit(-1);
            }
        }

        public static bool operator ==(PlayingCard lOperand, PlayingCard rOperand)
        {
            return lOperand.Equals(rOperand);
        }

        public static bool operator !=(PlayingCard lOperand, PlayingCard rOperand)
        {
            return !(lOperand == rOperand);
        }

        public override bool Equals(object obj)
        {
            Boolean retval = false;

            if (obj != null)
            {
                if(this.GetHashCode() == ((PlayingCard) obj).GetHashCode())
                    retval = true;
            }
            else
            {
                retval = false;
            }

            return retval;
        }

        public override int GetHashCode()
        {
            return ((int)Suit) * 32 + ((int)Rank);
        }

        public override string ToString()
        {
            String retval = "";

            switch(Rank)
            {
                case CardRank.Ace:
                    retval += "A";
                    break;
                case CardRank.Jack:
                    retval += "J";
                    break;
                case CardRank.Queen:
                    retval += "Q";
                    break;
                case CardRank.King:
                    retval += "K";
                    break;
                case CardRank.Joker:
                    retval += "!";
                    break;
                default:
                    retval += ((int)Rank).ToString();
                    break;

            }

            switch (Suit)
            {
                case CardSuit.Diamonds:
                    retval += "D";
                    break;
                case CardSuit.Clubs:
                    retval += "C";
                    break;
                case CardSuit.Hearts:
                    retval += "H";
                    break;
                case CardSuit.Spades:
                    retval += "S";
                    break;
            }

            return retval;
        }
    }
}
