using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyrinthGame.Entities;
using LabyrinthGame.Items;

namespace LabyrinthGame
{
    class MainCmd
    {
        private static Room[,] _gameField;
        private static Player _player;
        private static int _turnsLeft = 40;
        private static Random r = new Random();
        static void Main(string[] args)
        {
            // Preparing the game field
            while (true)
            {
                int choice;

                Console.WriteLine("Выберите размер поля:\n1 — 4x4\n2 — 7x7\n3 — 10x10");

                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice < 4)
                {
                    int tableLen = (choice - 1) * 3 + 4;

                    // Creating the field
                    _gameField = new Room[tableLen, tableLen];
                    for (int i = 0; i < tableLen; ++i)
                    {
                        for (int q = 0; q < tableLen; ++q)
                        {
                            _gameField[i,q] = new Room();
                        }
                    }

                    // Populating the field with keys
                    _gameField[r.Next(tableLen), r.Next(tableLen)].Items.Add(new RealKey());
                    _gameField[r.Next(tableLen), r.Next(tableLen)].Items.Add(new FakeKey());

                    // Populating the field with doors
                    _gameField[r.Next(tableLen), r.Next(tableLen)].Entities.Add(new Door());

                    // Populating the field with events
                    // TODO: refactor
                    List<RoomEvent> roomEvents = new List<RoomEvent>
                    {
                        new RoomEvent("Какое число больше: 10 или 20?", new List<string>{"20"}, null),
                        new RoomEvent("Что из перечисленного является цитрусом: лимон, авокадо, персик, лайм?", new List<string>{"лимон, лайм"}, null),
                        new RoomEvent("Загадка: висит груша, нельзя скушать. Что это?", new List<string>{"лампочка"}, new List<Item>{ new Shield() })
                    };

                    foreach (RoomEvent roomEvent in roomEvents)
                    {
                        _gameField[r.Next(tableLen), r.Next(tableLen)].Events.Add(roomEvent);
                    }

                    // Spawning the player
                    _player = new Player("Джон Доу");
                    _player.X = r.Next(tableLen);
                    _player.Y = r.Next(tableLen);

                    break;
                }
            }

            // Playing the game
            while (_turnsLeft >= 0)
            {
                Console.WriteLine("Что будете делать? (напишите help для помощи");

                string[] line = Console.ReadLine()?.Split(' ');

                if (line == null || line.Length < 1)
                {
                    continue;
                }

                line = Array.ConvertAll(line, s => s.ToLower());

                // Go in one of 4 possible directions
                if (line[0] == "go" && line.Length > 1)
                {
                    //
                }
                

                --_turnsLeft;
            }
        }
    }
}
