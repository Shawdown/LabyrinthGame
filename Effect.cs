namespace LabyrinthGame
{
    public abstract class Effect
    {
        private string Name { get; }
        private string ActivateIn { get; }

        protected Effect(string name, string activateIn)
        {
            Name = name;
            ActivateIn = activateIn;
        }
    }
}