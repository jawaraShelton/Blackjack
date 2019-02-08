﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Blackjack
{
    interface IBlackjackHand : IHand
    {
        Boolean Standing { get; set; }

        void Stand();
        Boolean IsBlackjack();
    
    }

    class BlackjackHand : Hand, IBlackjackHand
    {
        public Boolean Bust {
            get
            {
                return Value() > 21;
            }
        }

        public Boolean Standing {
            get
            {
                return Standing;
            }
            set
            {
                Standing = value;
            }
        }

        public void Stand()
        {
            Standing = true;
        }

        public override int Value() 
        {
            List<String> Aces = new List<String>();
            int returnValue = 0;

            foreach (String Card in Cards)
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

            if (Cards.Count == 2)
            {
                foreach (String Card in Cards)
                    nbcLock += Card.Substring(0, 1);

                if ("A1|1A AJ|JA AQ|QA AK|KA".Contains(nbcLock))
                    retval = true;
            }

            return retval;
        }

        public bool IsBlackjack()
        {
            throw new NotImplementedException();
        }
    }
}