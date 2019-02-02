using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Blackjack
{
    class BlackjackHand : Hand
    {
        public override int Value() 
        {
            List<String> Aces = new List<String>();
            int returnValue = 0;

            foreach (String Card in hand)
            {
                String CardValue = Card.Substring(0, 1);

                if ("1JKQ!".Contains(CardValue))
                    returnValue += 10;
                else if (CardValue.Equals("A"))
                {
                    Aces.Add(CardValue);
                }
                else
                    returnValue += int.Parse(CardValue);
}

            if (Aces.Count > 0)
                if (Aces.Count == 1)
                {
                    if (returnValue <= 10)
                        returnValue += 11;
                    else
                        returnValue += 1;
                }
                else
                {
                    if (returnValue >= 10)
                    {
                        returnValue += Aces.Count;
                    }
                    else
                    {
                        while (Aces.Count > 0)
                        {
                            if ((returnValue + 11 + (Aces.Count - 1)) > 21)
                                returnValue++;
                            else
                                returnValue += 11;

                            Aces.Remove("A");
                        }
                    }
                }

            return (returnValue);
        }

        public Boolean IsBlackJack()
        {
            Boolean retval = false;
            String nbcLock = "";

            if (hand.Count == 2)
            {
                foreach (String Card in hand)
                    nbcLock += Card.Substring(0, 1);

                if ("A1|1A AJ|JA AQ|QA AK|KA".Contains(nbcLock))
                    retval = true;
            }

            return retval;
        }
    }
}
