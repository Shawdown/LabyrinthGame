using System;
using System.Collections.Generic;

namespace LabyrinthGame
{
    public class Player : Creature
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Effect> Effects { get; set; } = new List<Effect>();

        public Player(string name) : base(name)
        {
        }

        public void PrintInventory()
        {
            if (Items.Count < 1)
            {
                Console.WriteLine("Ваш инвентарь пуст.");
                return;
            }

            Console.WriteLine("В вашем инвентаре есть следующие предметы:");
            foreach (Item item in Items)
            {
                Console.WriteLine($"[{item.Name}]");
            }
        }
    }
}