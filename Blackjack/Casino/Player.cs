using System;
using System.Collections.Generic;

namespace Blackjack
{
    interface IPlayer
    {
        List<IHand> PlayerHand { get; set; }
        String PlayerName { get; set; }

        void NewHand();
        void AddToHand(String Card);
        String ShowHand();
    }
}
