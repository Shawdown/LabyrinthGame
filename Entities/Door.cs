using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGame.Entities
{
    class Door : Entity
    {
        public Door() : base("Дверь наружу")
        {
        }

        public override bool Use(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
