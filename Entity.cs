namespace LabyrinthGame
{
    public abstract class Entity : IUsable
    {
        public string Name { get; }

        protected Entity(string name)
        {
            Name = name;
        }

        public abstract bool Use(Player p);
    }
}