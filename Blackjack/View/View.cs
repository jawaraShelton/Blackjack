using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public interface IView
    {
        void ModelChanged(Boolean viewOnly = false);
    }
}
