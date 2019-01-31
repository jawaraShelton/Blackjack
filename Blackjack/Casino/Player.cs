using System;

namespace Blackjack
{
    interface IPlayer
    {
        Hand PlayerHand { get; set; }
        String PlayerName { get; set; }

        void NewHand();
        void AddToHand(String Card);
        String ShowHand();
    }
}
