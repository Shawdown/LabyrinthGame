using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthGame.Items;

namespace LabyrinthGame.Entities
{
    class Door : Entity
    {
        public Door() : base("Дверь наружу")
        {
        }

        public override bool Use(Player p)
        {
            Console.WriteLine("Какой предмет вы хотите использовать?");
            string itemName = Console.ReadLine()?.ToLower();

            foreach (Item item in p.Items)
            {
                if (item.Name.ToLower() == itemName)
                {
                    if (item is RealKey)
                    {
                        Console.WriteLine("Вы использовали настоящий ключ и вышли наружу.");
                        return true;
                    }
                    else if (item is FakeKey)
                    {
                        Console.WriteLine("Вы использовали ложный ключ и пали в забвение.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Этот предмет нельзя использовать.");
                        return false;
                    }
                }
            }

            Console.WriteLine("У вас нет такого предмета.");
            return false;
        }
    }
}
