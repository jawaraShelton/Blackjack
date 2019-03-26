using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Application
{
    class BlackjackConsoleViewAddressed : IView
    {
        private BlackjackModel Model;
        private BlackjackController Controller;

        public BlackjackConsoleViewAddressed()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public BlackjackConsoleViewAddressed(BlackjackModel Model, BlackjackController Controller)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            this.Model = Model;
            this.Controller = Controller;
        }

        public void LinkModel(BlackjackModel Model)
        {
            this.Model = Model;
        }

        public void LinkController(BlackjackController Controller)
        {
            this.Controller = Controller;
        }

        public void ModelChanged()
        {
            ModelChanged(false);
        }

        public void ModelChanged(Boolean viewOnly = false)
        {
            if (Model != null)
                Show(viewOnly);
            else
                throw new InvalidOperationException("The View lacks its corresponding Model.");
        }

        public void Show(Boolean viewOnly)
        {

            if (Model.GetFlavorText().Count() > 0)
            {
                Console.WriteLine();
                foreach (String str in Model.GetFlavorText())
                    Console.WriteLine(str);
            }

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("--------------");
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Dealer's Hand : {0}", Model.GetDealerHand());
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Player's Hand : {0}", Model.GetPlayerHand());
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Current Wager : {0}", Model.GetWager().ToString());
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("-----");
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Cash Available: {0}", Model.GetCashAvailable().ToString());
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("--------------");
            Console.SetCursorPosition(0, 8);

            if (Model.GetResultText().Count() > 0)
            {
                Console.WriteLine();
                foreach (String str in Model.GetResultText())
                    Console.WriteLine("    {0}", str);
            }

            Console.WriteLine();
            if(!viewOnly)
                GetCommand();
        }

        private void GetCommand()
        {
            String command;

            if (Model != null)
                do
                {
                    Console.WriteLine("Available Commands: {0}", Model.AvailableCommands());
                    Console.Write("Ready, {0}: ", Model.GetPlayer().PlayerName);
                    command = Console.ReadLine().ToLower();
                } while (!Controller.Execute(command));
            else
                throw new InvalidOperationException("The View lacks a corresponding Controller.");
        }
    }
}
