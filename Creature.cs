namespace LabyrinthGame
{
    public class Creature
    {
        public string Name { get; }
        public int X { get; set; }
        public int Y { get; set; }

            public Creature(string name)
        {
            Name = name;
        }
    }
}