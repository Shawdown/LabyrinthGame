using System.Security.Policy;

namespace LabyrinthGame
{
    public interface IUsable
    {
        bool Use(Player p);
    }
}