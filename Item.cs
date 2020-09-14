namespace LabyrinthGame
{
    public abstract class Item : IUsable
    {
        public string Name { get; }

        protected Item(string name)
        {
            Name = name;
        }
        public abstract bool Use(Player p);
    }
}