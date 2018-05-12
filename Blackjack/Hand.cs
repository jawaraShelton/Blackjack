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

        public int Value()
        {
            int returnValue=0;

            foreach (String Card in hand)
            {
                String CardValue = Card.Substring(0, 1);

                if ("1JKQ".Contains(CardValue))
                    returnValue += 10;
                else if (CardValue.Equals("A"))
                {
                    if (returnValue < 11)
                        returnValue += 11;
                    else
                        returnValue += 1;
                }
                else
                    returnValue += int.Parse(CardValue);
            }

            return (returnValue);
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
    }
}
