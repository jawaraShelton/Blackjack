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
            //  >>>>>[  ShoeRESTful automatically incorporates 6 decks--all of which 
            //          handled by the Deck Of Cards API.
            //          -----
            // ShoeRESTful Shoe = new ShoeRESTful();

            //  >>>>>[  The Shoe class does NOT automatically incorporate 6 decks. 
            //          I've a bit more flexibility in how the local Shoe class is
            //          created.
            //          -----
            Shoe Shoe = new Shoe(6);

            BlackjackDealer Dealer = new BlackjackDealer(Shoe);
            BlackjackPlayer Player = new BlackjackPlayer("Player 1", 500);

            BlackjackModel Model = new BlackjackModel(Dealer, Player);
            BlackjackController Controller = new BlackjackController(Model);
            BlackjackView View = new BlackjackView(Model, Controller);

            Model.LinkView(View);
            View.ModelChanged();
        }
    }
}
