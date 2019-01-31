using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    abstract class CasinoDealer: Dealer
    {
        public override void Shuffle(int numberOfDecks = 4)
        {
            //  >>>>>[  Shuffle the cards by creating a new shoe. The shoe 
            //          randomizes the cards on creation.
            //          - jds 2019.01.23
            //          -----
            shoe = new Shoe(numberOfDecks);
        }
    }
}
