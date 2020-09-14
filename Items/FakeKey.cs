using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGame.Items
{
    class FakeKey : Item
    {
        public FakeKey() : base("Ложный ключ")
        {
        }

        public override bool Use(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
