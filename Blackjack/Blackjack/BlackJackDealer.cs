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
            me = new BlackjackPlayer();
            PlayerHand = new BlackjackHand();
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
            String returnValue = PlayerHand.ToString();

            if(!reveal && !returnValue.Equals("EMPTY"))
                returnValue = "??" + returnValue.Substring(returnValue.IndexOf(' '));

            return (returnValue);
        }

        public override String PlayHand()
        {
            reveal = true;

            while (PlayerHand.Value() <= 17)
                PlayerHand.Add(Deal());

            return PlayerHand.Show() + "\n" + (PlayerHand.Value() > 21 ? "Dealer Busts." : "");
        }
    }
}
