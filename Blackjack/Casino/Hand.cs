using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Hand
    {
        public List<String> hand = new List<String>();
        
        public void Add(String Card)
        {
            hand.Add(Card);
        }

        public virtual int Value()
        {
            return 0;
        }

        public String Show()
        {
            String returnValue = "";

            foreach (String Card in hand)
                returnValue += Card + " ";

            return (returnValue.Trim(' '));
        }

        public void Clear()
        {
            hand.Clear();
        }

        public override string ToString()
        {
            if (hand.Count > 0)
                return (Show());
            else
                return "EMPTY";
        }
        
    }
}
