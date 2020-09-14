using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGame.Items
{
    class RealKey : Item
    {
        public RealKey() : base("Настоящий ключ")
        {
        }

        public override bool Use(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
