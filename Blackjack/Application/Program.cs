using Blackjack.Blackjack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            BlackjackHand Hand = new BlackjackHand();
            BlackjackDealer Dealer = new BlackjackDealer();
            BlackjackPlayer Player = new BlackjackPlayer(Hand, 500, "Player 1");

            BlackjackModel Model = new BlackjackModel(Dealer, Player);
            BlackjackController Controller = new BlackjackController(Model);
            BlackjackView View = new BlackjackView(Model, Controller);

            Model.LinkView(View);
            View.ModelChanged();
        }
    }
}
