using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface Observer
    {
        void updateBar(PlayerBase player);
    }
}
