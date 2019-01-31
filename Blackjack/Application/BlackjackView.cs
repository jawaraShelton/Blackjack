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

        public BlackjackView(BlackjackModel Model, BlackjackController Controller)
        {
            this.Model = Model;
            this.Controller = Controller;
        }

        public void ModelChanged()
        {
            Show();
        }

        private void Show()
        {

        }

        private void GetCommand()
        {
            String command;
            do
            {
                Console.WriteLine("Available Commands: hit | stand | double down {0}", Model.GetPlayer().CanSurrender ? " | surrender" : "");
                Console.Write("Ready, {0}: ", Model.GetPlayer().PlayerName);
                command = Console.ReadLine().ToLower();
            } while (!Controller.Execute(command));
        }
    }
}
