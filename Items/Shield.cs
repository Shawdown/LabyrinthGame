using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGame.Items
{
    class Shield : Item
    {
        public Shield() : base("Щит")
        {
        }

        public override bool Use(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
