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
    }
}