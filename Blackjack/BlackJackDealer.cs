using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class BlackjackDealer: CasinoDealer
    {
        public override String Deal()
        {
            return (shoe.Draw());
        }

        public override String ShowHand()
        {
            String returnValue = hand.Show();

            returnValue = "??" + returnValue.Substring(returnValue.IndexOf(' '));
            return (returnValue);
        }

        public override String PlayHand()
        {
            while (hand.Value() <= 17)
                hand.Add(Deal());

            return hand.Show() + "\n" + (hand.Value() > 21 ? "Dealer Busts." : "");
        }

        public override void Go()
        {
            throw new NotImplementedException();
        }
    }
}
