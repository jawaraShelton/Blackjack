using Blackjack.Blackjack;

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

            //  >>>>>[  To use the straight Console/CLI View
            //          -----
            //          BlackjackConsoleView View = new BlackjackConsoleView(Model, Controller);

            //  >>>>>[  To use the "Fake-Curses" View
            //          -----
            BlackjackCursesView View = new BlackjackCursesView(Model, Controller);
            
            Model.LinkView(View);
            View.ModelChanged();
        }
    }
}
