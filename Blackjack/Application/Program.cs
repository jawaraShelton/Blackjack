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

            BlackjackDealer Dealer = null;

            /*
            switch (PlayerGameSelect())
            {
                case 1:
                    //  >>>>>[  Player has selected the option to play a standard Blackjack game.
                    //          This choice allows them to select between the Deck of Cards API
                    //          or my own code. 
                    //
                    //          Which they choose doesn't necessarily make any differences, I 
                    //          just wanted to throw a little RESTful into the mix.
                    //          -----
            */
                    switch (PlayerSelectShoeType())
                    {
                    case 1:
                        //  >>>>>[  The Shoe class does NOT automatically incorporate 6 decks. 
                        //          I've a bit more flexibility in how the local Shoe class is
                        //          created.
                        //          -----
                        Dealer = new BlackjackDealer(new Shoe(6));
                        break;

                    case 2:
                        //  >>>>>[  ShoeRESTful automatically incorporates 6 decks--all of which 
                        //          handled by the Deck Of Cards API.
                        //          -----
                        Dealer = new BlackjackDealer(new ShoeRESTful());
                        break;
                    }
            /*
                    break;
                case 2:
                    //  >>>>>[  Player has selected the option to play Spanish 21. For the moment,
                    //          that means they're stuck with my classes, so no REST API here.
                    //          -----
                    List<PlayingCard> CardsToExclude = new List<PlayingCard>()
                    {
                        new PlayingCard(CardSuit.Clubs, CardRank.Ten),
                        new PlayingCard(CardSuit.Diamonds, CardRank.Ten),
                        new PlayingCard(CardSuit.Hearts, CardRank.Ten),
                        new PlayingCard(CardSuit.Spades, CardRank.Ten)
                    };

                    Dealer = new BlackjackDealer(new Shoe(6, CardsToExclude));
                    break;
            }
            */

            BlackjackPlayer Player = new BlackjackPlayer("Player 1", 500);

            BlackjackModel Model = new BlackjackModel(Dealer, Player);
            BlackjackController Controller = new BlackjackController(Model);
            BlackjackView View = new BlackjackView(Model, Controller);

            Model.LinkView(View);
            View.ModelChanged();
        }

        static Int16 PlayerSelectShoeType()
        {
            Int16 retval=0;

            while(retval != 1 && retval != 2)
            {
                Console.WriteLine("1. Use cards drawn on local machine.");
                Console.WriteLine("2. Use cards drawn via the Deck of Cards API.");
                Console.Write("> ");
                Int16.TryParse(Console.ReadLine(), out retval);
            }

            return retval;
        }

        static Int16 PlayerGameSelect()
        {
            Int16 retval = 0;

            while (retval != 1 && retval != 2)
            {
                Console.WriteLine("1. Standard Casino Blackjack.");
                Console.WriteLine("2. Spanish 21.");
                Console.Write("> ");
                Int16.TryParse(Console.ReadLine(), out retval);
            }

            return retval;
        }
    }
}
