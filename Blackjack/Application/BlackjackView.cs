using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Application
{
    class BlackjackView : IView
    {
        private BlackjackModel Model;
        private BlackjackController Controller;

        public BlackjackView()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public BlackjackView(BlackjackModel Model, BlackjackController Controller)
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
            if (Model != null)
                Show();
            else
                throw new InvalidOperationException("The View lacks its corresponding Model.");
        }

        public void Show()
        {
            if (Model.GetFlavorText().Count() > 0)
            {
                Console.WriteLine();
                foreach (String str in Model.GetFlavorText())
                    Console.WriteLine(str);
            }

            Console.WriteLine("-----");
            Console.WriteLine("Dealer's Hand: {0}", Model.GetDealerHand());
            Console.WriteLine("Player's Hand: {0}", Model.GetPlayerHand());
            Console.WriteLine("Your Wager   : {0}", Model.GetWager());

            if (Model.GetResultText().Count() > 0)
            {
                Console.WriteLine();
                foreach (String str in Model.GetResultText())
                    Console.WriteLine(str);
            }

            Console.WriteLine();
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
