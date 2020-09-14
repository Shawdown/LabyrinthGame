using System.Collections.Generic;

namespace LabyrinthGame
{
    public class Room
    {
        public List<RoomEvent> Events { get; set; } = new List<RoomEvent>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Entity> Entities { get; set; } = new List<Entity>();
    }
}