using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Application
{
    class BlackjackController : IController
    {
        private readonly BlackjackModel model;

        public BlackjackController(BlackjackModel model)
        {
            this.model = model;
        }

        public bool Execute(string command)
        {
            bool retval = true;
            String[] args = command.Split(' ');
            if (!args[0].Equals(""))
            {
                switch (args[0].Substring(0, 1))
                {
                    case "b":
                        decimal wager;
                        try
                        {
                            decimal.TryParse(args[1], out wager);
                        }
                        catch
                        {
                            wager = 0;
                        }
                        model.Bet(wager);
                        break;
                    case "d":
                        model.DoubleDown();
                        break;
                    case "h":
                        model.Hit();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    case "s":
                        if(args[0].Length < 2)
                        {
                            ReturnError();
                        }
                        else
                        { 
                            switch (args[0].Substring(0, 2))
                            {
                                case "sp":
                                    if (model.GetPlayer().CanSplit)
                                    {
                                        model.Split();
                                    }
                                    else
                                    {
                                        ReturnError();
                                        retval = false;
                                    }
                                    break;
                                case "st":
                                    model.Stand();
                                    break;
                                case "su":
                                    model.Surrender();
                                    break;
                                default:
                                    ReturnError();
                                break;
                            }
                        }
                        break;
                    default:
                        ReturnError();
                        break;
                }
            }
            else
            {
                ReturnError();
            }

            return retval;

            void ReturnError()
            {
                //  >>>>>[  Invalid Command. Notify View command failed.
                //          -----
                Console.WriteLine("Command invalid. Retry.");

                retval = false;
            }
        }
    }
}
