using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new BlackjackDealer();

            dealer.AddPlayer(new BlackjackPlayer("Player 1", 500));
            dealer.Go();
        }
    }
}
