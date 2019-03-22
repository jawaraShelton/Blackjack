using System;
using System.Collections.Generic;

namespace Blackjack
{
    interface IHand
    {
        List<string> Cards { get; set; }

        void Add(String Card);
        int Value();
        String Show();
        void Clear();

    }

    class Hand : IHand
    {
        public List<string> Cards { get; set; }

        public Hand()
        {
            Cards = new List<string>();
        }

        public void Add(String Card)
        {
            Cards.Add(Card);
        }

        public virtual int Value()
        {
            return 0;
        }

        public String Show()
        {
            String returnValue = "";

            foreach (String Card in Cards)
                returnValue += Card + " ";

            return (returnValue.Trim(' '));
        }

        public void Clear()
        {
            Cards.Clear();
        }

        public override string ToString()
        {
            if (Cards != null && Cards.Count > 0)
                return (Show());
            else
                return "EMPTY";
        }
        
    }
}
