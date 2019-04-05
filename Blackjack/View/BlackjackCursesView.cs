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

        private int width = 118;

        //  >>>>>[  The score window currently has a fixed height of 7.
        //          -----
        readonly int SCORE_TOP = 19;
        readonly int SCORE_LEFT = 0;

        //  >>>>>[  The scroll window currently has a fixed height of 18.
        //          -----
        readonly int SCROLL_TOP = 0;
        readonly int SCROLL_LEFT = 0;

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
            UpdatePlayerScore();
            UpdateScrollWindow();

            //  >>>>>[  Display commands and get user input (if !viewOnly)
            //          -----
            if (!viewOnly)
                GetCommand();
        }

        private void UpdatePlayerScore()
        {
            //  >>>>>[  Depending on the game's state (actively playing the hand, hand has
            //          concluded, etc...) the dominant color used to display data should 
            //          should be able to switch. Modification is in progress.
            //          -----
            ConsoleColor BRIGHT = ConsoleColor.White;
            ConsoleColor DIM = ConsoleColor.DarkGray;
              
            ConsoleColor DOMINANT = BRIGHT;

            //  >>>>>[  Draw the "crossbars" bounding the various sections of the screen.
            //          -----
            OverWrite(SCORE_LEFT, SCORE_TOP, new string('-', width), ConsoleColor.Gray);
            OverWrite(SCORE_LEFT, SCORE_TOP+6, new string('-', width), ConsoleColor.Gray);

            //  >>>>>[  Display the Dealer's hand. Nothing really special 
            //          needs to be done...
            //          -----
            OverWrite(SCORE_LEFT, SCORE_TOP+2, "Dealer's Hand : " + Model.GetDealerHand(), DOMINANT, 47);

            //  >>>>>[  Display the Player's hand. The player has the ability
            //          to split their hand, so each individual hand must be
            //          displayed. 
            //
            //          Split hands not currently being played will be gray 
            //          in color. The active hand will be White.
            //          -----
            OverWrite(SCORE_LEFT+48, SCORE_TOP+2, "Player's Hand : ", DOMINANT, 47);
            if (!Model.GetPlayerHand().Equals("EMPTY"))
            {
                int y = 2;
                foreach (string hand in Model.GetPlayerHand().Split('|'))
                    OverWrite(SCORE_LEFT+64, SCORE_TOP+(y++), hand.Trim().Substring(0, 2).Equals(">>") ? hand.Trim().Substring(3) : hand.Trim(),
                        hand.Trim().Substring(0, 2).Equals(">>") ? DOMINANT : Model.GetPlayerHand().Split('|').Count() > 1 ? DIM : DOMINANT, 31);
            }
            else
            {
                for (int y = 2; y < 6; y++)
                    OverWrite(SCORE_LEFT+64, SCORE_TOP+y, "", DOMINANT, 31);

                OverWrite(SCORE_LEFT+64, SCORE_TOP+2, Model.GetPlayerHand(), DOMINANT, 31);
            }

            //  >>>>>[  Display the current wager.
            //          -----
            OverWrite(SCORE_LEFT+48, SCORE_TOP+1, "Current Wager : " + Model.GetWager().ToString("C"), DOMINANT, ("Current Wager : " + Model.GetWager().ToString("C")).Length + 4);

            //  >>>>>[  Display available cash. 
            //          If cash available = 0 then color should be red.
            //          -----
            OverWrite(SCORE_LEFT + 88, SCORE_TOP + 1, "Cash Available: ", DOMINANT, ("Cash Available: ").Length);
            OverWrite(SCORE_LEFT + 105, SCORE_TOP + 1, Model.GetCashAvailable().ToString("C"), Model.GetCashAvailable() == 0 ? ConsoleColor.Red : DOMINANT, 14);
        }

        private void UpdateScrollWindow()
        {
            //  >>>>>[  Draw the bounding boxes
            //          -----
            OverWrite(SCROLL_LEFT, SCROLL_TOP, new string('-', width), ConsoleColor.Gray);
            OverWrite(SCROLL_LEFT, SCROLL_TOP + 19, new string('-', width), ConsoleColor.Gray);

            //  >>>>>[  Display the flavor and result text
            //          -----
            foreach (String str in Model.GetFlavorText())
                OutputText.Add(str);

            foreach (String str in Model.GetResultText())
                OutputText.Add(str);

            while (OutputText.Count > 18)
                OutputText.RemoveAt(0);

            int Y = 1;
            foreach (String str in OutputText)
                OverWrite(SCROLL_LEFT, SCROLL_TOP+(Y++), "  " + str, ConsoleColor.DarkGray);
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
