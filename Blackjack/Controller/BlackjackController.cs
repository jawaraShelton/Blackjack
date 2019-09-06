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
                switch (args[0].Substring(0, 1).ToLower())
                {
                    case "b":
                        if(model.CommandAvailable("bet"))
                        {
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
                        }
                        else
                            retval = false;
                        break;
                    case "d":
                        if (model.CommandAvailable("double down"))
                            model.DoubleDown();
                        else
                            retval = false;
                        break;
                    case "h":
                        if (model.CommandAvailable("hit"))
                            model.Hit();
                        else
                            retval = false;
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    case "r":
                        model.Restart();
                        break;
                    case "s":
                        if(args[0].Length < 2)
                        {
                            ReturnError();
                        }
                        else
                        { 
                            switch (args[0].Substring(0, 2).ToLower())
                            {
                                case "sp":
                                    if (model.CommandAvailable("split"))
                                    {
                                        if (model.GetPlayer().CanSplit)
                                        {
                                            model.Split();
                                        }
                                        else
                                        {
                                            ReturnError();
                                            retval = false;
                                        }
                                    }
                                    else
                                    {
                                        retval = false;
                                    }
                                    break;
                                case "st":
                                    if (model.CommandAvailable("stand"))
                                        model.Stand();
                                    else
                                        retval = false;
                                    break;
                                case "su":
                                    if (model.CommandAvailable("surrender"))
                                        model.Surrender();
                                    else
                                        retval = false;
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
                Console.Write(new string(' ', 80));
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
