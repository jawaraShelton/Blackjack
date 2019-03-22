using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Model
    {
        private List<IView> views = new List<IView>();

        public void AddView(IView view)
        {
            this.views.Add(view);
        }

        public void Changed()
        {
            foreach (IView v in views)
                v.ModelChanged();
        }
    }
}
