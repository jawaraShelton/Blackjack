using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    interface IDeck
    {
        void Build();
        void Shuffle();
        String Draw();
        Boolean Empty();
    }
}
