using System;
using System.Linq;
using System.Collections.Generic;


namespace Blackjack.Application
{
    class BlackjackCursesView : IView
    {
        private BlackjackModel Model;
        private BlackjackController Controller;
        private List<String> OutputText;

        public BlackjackCursesView()
        {
            SetUsUp();
        }

        public BlackjackCursesView(BlackjackModel Model, BlackjackController Controller)
        {
            this.Model = Model;
            this.Controller = Controller;

            SetUsUp();
        }

        private void SetUsUp()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            OutputText = new List<String>();
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
            //  >>>>>[  Display the Dealer's hand. Nothing really special 
            //          needs to be done...
            //          -----
            OverWrite(0, 2, "Dealer's Hand : " +  Model.GetDealerHand(), ConsoleColor.White, 47);

            //  >>>>>[  Display the Player's hand. The player has the ability
            //          to split their hand, so each individual hand must be
            //          displayed. 
            //
            //          Split hands not currently being played will be gray 
            //          in color. The active hand will be White.
            //          -----
            OverWrite(48, 2, "Player's Hand : ", ConsoleColor.White, 47);
            if (!Model.GetPlayerHand().Equals("EMPTY"))
            {
                int y = 2;
                foreach (string hand in Model.GetPlayerHand().Split('|'))
                    OverWrite(64, y++, hand.Trim().Substring(0, 2).Equals(">>") ? hand.Trim().Substring(3) : hand.Trim(),
                        hand.Trim().Substring(0, 2).Equals(">>") ? ConsoleColor.White : Model.GetPlayerHand().Split('|').Count() > 1 ? ConsoleColor.DarkGray : ConsoleColor.White, 31);
            }
            else
            {
                for (int y = 2; y < 6; y++)
                    OverWrite(64, y, "", ConsoleColor.White, 31);

                OverWrite(64, 2, Model.GetPlayerHand(), ConsoleColor.White, 31);
            }

            //  >>>>>[  Display the current wager.
            //          -----
            OverWrite(48, 1, "Current Wager : " +  Model.GetWager().ToString("C"), ConsoleColor.White, ("Current Wager : " + Model.GetWager().ToString("C")).Length + 4);
            
            //  >>>>>[  Display available cash. 
            //          If cash available = 0 then color should be red.
            //          -----
            OverWrite(88, 1, "Cash Available: " , ConsoleColor.White, ("Cash Available: ").Length);
            OverWrite(105, 1, Model.GetCashAvailable().ToString("C"), Model.GetCashAvailable() == 0 ? ConsoleColor.Red : ConsoleColor.White, 14); 

            //  >>>>>[  Draw the "crossbars" bounding the various sections of the screen.
            //          -----
            int width = 118;
            OverWrite(0, 0, new string('-', width), ConsoleColor.Gray);
            OverWrite(0, 6, new string('-', width), ConsoleColor.Gray);
            OverWrite(0, 25, new string('-', width), ConsoleColor.Gray);

            //  >>>>>[  Display the flavor and result text
            //          -----
            foreach (String str in Model.GetFlavorText())
                OutputText.Add(str);
            
            foreach (String str in Model.GetResultText())
                OutputText.Add(str);

            while(OutputText.Count > 18)
                OutputText.RemoveAt(0);

            int Y = 7;
            foreach (String str in OutputText)
                OverWrite(0, Y++, "  " + str, ConsoleColor.DarkGray);

            //  >>>>>[  Display commands and get user input (if !viewOnly)
            //          -----
            if (!viewOnly)
                GetCommand();
        }

        private void GetCommand()
        {
            String command;

            if (Model != null)
                do
                {
                    OverWrite(0, 26, "Available Commands: " + Model.AvailableCommands(), ConsoleColor.Blue);
                    OverWrite(0, 27, "Ready, " + Model.GetPlayer().PlayerName + ": ", ConsoleColor.Green);

                    command = Console.ReadLine().ToLower();
                } while (!Controller.Execute(command));
            else
                throw new InvalidOperationException("The View lacks a corresponding Controller.");
        }

        private void OverWrite(int X, int Y, String Output, ConsoleColor Color = ConsoleColor.White, int WipeLength = 96)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(new string(' ', WipeLength));
            Console.SetCursorPosition(X, Y);

            Console.ForegroundColor = Color;
            Console.Write(Output);
            Console.ResetColor();
        }
    }
}
