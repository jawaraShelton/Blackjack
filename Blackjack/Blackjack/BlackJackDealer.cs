using System;
using Blackjack.Blackjack;

namespace Blackjack
{
    class BlackjackDealer: CasinoDealer
    {
        public BlackjackPlayer me;
        private Boolean reveal;

        public BlackjackDealer()
        {
            me = new BlackjackPlayer(new BlackjackHand(), 0);
            Shuffle();
            reveal = false;
        }

        public override String Deal()
        {
            return (shoe.Draw());
        }

        public void ResetReveal()
        {
            reveal = false;
        }

        public override String ShowHand()
        {
            String returnValue = me.PlayerHand[0].ToString();

            if(!reveal && !returnValue.Equals("EMPTY"))
                returnValue = "??" + returnValue.Substring(returnValue.IndexOf(' '));

            return (returnValue);
        }

        public override String PlayHand()
        {
            reveal = true;

            while (PlayerHand[0].Value() <= 17)
                PlayerHand[0].Add(Deal());

            return PlayerHand[0].Show() + "\n" + (PlayerHand[0].Value() > 21 ? "Dealer Busts." : "");
        }
    }
}
