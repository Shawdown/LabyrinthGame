using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        private static int _turnsLeft;
        private static Random r = new Random();
        private static int _tableLen;

        static void Main(string[] args)
        {
            // Preparing the game field
            Init();

            // Playing the game
            while (_turnsLeft >= 0)
            {
                Room room = GetCurrentRoom();
                // Dealing with room events
                while (room.Events.Count > 0)
                {
                    RoomEvent roomEvent = room.Events.First();

                    Console.WriteLine("Вам нужно решить загадку:");
                    Console.WriteLine(roomEvent.Question);
                    Console.WriteLine("Ваш ответ:");

                    string answer = Console.ReadLine().ToLower();

                    if (roomEvent.CheckAnswer(answer))
                    {
                        Console.WriteLine("Вы ответили правильно.");
                    }
                    else
                    {
                        Console.WriteLine($"Вы ошиблись и потеряли следующее количество ходов: { roomEvent.Punishment }.");
                    }

                    room.Events.RemoveAt(0);
                }

                Console.Write($"\n\nВы находитесь в комнате [{_player.Y},{_player.X}].");

                if (room.Items.Count > 0)
                {
                    Console.Write("\nЗдесь есть следующие предметы:\n");
                    foreach (Item item in room.Items)
                    {
                        Console.Write($"[{item.Name}]\n");
                    }
                }
                else
                {
                    Console.Write("\nЗдесь нет предметов.");
                }

                if (room.Entities.Count > 0)
                {
                    Console.Write("\nЗдесь есть следующие объекты:\n");
                    foreach (Entity entity in room.Entities)
                    {
                        Console.Write($"[{entity.Name}]\n");
                    }
                }
                else
                {
                    Console.Write("\nЗдесь нет объектов.");
                }

                Console.WriteLine("\nЧто будете делать? (напишите help для помощи)\n");

                string[] line = Console.ReadLine()?.Split(' ');

                if (line == null || line.Length < 1)
                {
                    continue;
                }

                line = Array.ConvertAll(line, s => s.ToLower());

                // GO: go in one of the 4 possible directions
                if (line[0] == "go" && line.Length > 1)
                {
                    // NORTH
                    if (line[1] == "north")
                    {
                        --_player.Y;
                    }
                    // EAST
                    else if (line[1] == "east")
                    {
                        ++_player.X;
                    }
                    // SOUTH
                    else if (line[1] == "south")
                    {
                        ++_player.Y;
                    }
                    // WEST
                    else if (line[1] == "west")
                    {
                        --_player.X;
                    }

                    // Checking if player is out of bounds
                    if (_player.X >= _tableLen) _player.X = 0;
                    else if (_player.X < 0) _player.X = _tableLen - 1;
                    if (_player.Y >= _tableLen) _player.Y = 0;
                    else if (_player.Y < 0) _player.Y = _tableLen - 1;

                    --_turnsLeft;
                }
                // INFO: displays the remaining turns and locations of keys
                else if (line[0] == "info")
                {
                    int realKeyX = -1, realKeyY = -1;
                    int fakeKeyX = -1, fakeKeyY = -1;

                    bool keysFound = false;

                    // Finding the location of keys
                    for (int y = 0; y < _gameField.GetLength(0) && !keysFound; ++y)
                    {
                        for (int x = 0; x < _gameField.GetLength(1) && !keysFound; ++x)
                        {
                            foreach (Item roomItem in _gameField[y,x].Items)
                            {
                                if (roomItem is RealKey)
                                {
                                    realKeyY = y;
                                    realKeyX = x;

                                }
                                else if (roomItem is FakeKey)
                                {
                                    fakeKeyY = y;
                                    fakeKeyX = x;
                                }

                                if (fakeKeyY >= 0 && realKeyY >= 0)
                                {
                                    keysFound = true;
                                    break;
                                }
                            }
                        }
                    }

                    Console.WriteLine($"У вас осталось {_turnsLeft} ходов." +
                                      $"\nВы находитесь в комнате [{_player.Y},{_player.X}]" +  
                                      $"\n1-й ключ находится в комнате [{realKeyY}, {realKeyX}]" +
                                      $"\n2-й ключ находится в комнате [{fakeKeyY}, {fakeKeyX}]");

                    _player.PrintInventory();
                }
                // PICK UP: picks up the item
                else if (line[0] == "pick" && line[1] == "up" && line.Length > 2)
                {
                    string itemName = "";

                    for (int i = 2; i < line.Length; ++i)
                    {
                        itemName += line[i] + " ";
                    }

                    itemName = itemName.TrimEnd(' ');

                    bool itemFound = false;

                    for (int i = 0; i < room.Items.Count; ++i)
                    {
                        if (room.Items[i].Name.ToLower() == itemName)
                        {
                            _player.Items.Add(room.Items[i]);
                            room.Items.RemoveAt(i);
                            itemFound = true;
                            
                        }
                    }

                    Console.WriteLine(itemFound ? $"Вы забрали {itemName}." : "Нет предмета с таким именем.");
                }
                // USE: use entity in the room or inventory item
                else if (line[0] == "use" && line.Length > 1)
                {
                    string objectName = "";

                    for (int i = 1; i < line.Length; ++i)
                    {
                        objectName += line[i] + " ";
                    }

                    objectName = objectName.TrimEnd(' ').ToLower();
                    bool found = false;
                    bool escaped = false;

                    // Checking inventory
                    for (int i = 0; i < _player.Items.Count; ++i)
                    {
                        if (_player.Items[i].Name.ToLower() == objectName)
                        {
                            _player.Items[i].Use(_player);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        // Checking entities in the room
                        for (int i = 0; i < room.Entities.Count; ++i)
                        {
                            if (room.Entities[i].Name.ToLower() == objectName)
                            {
                                _player.PrintInventory();

                                if (room.Entities[i] is Door)
                                {
                                    Console.WriteLine("Вы стоите перед дверью.");
                                }

                                escaped = room.Entities[i].Use(_player);
                                found = true;
                            }
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Нет предмета или объекта в комнате с таким именем.");
                    }
                    else if (escaped)
                    {
                        Console.WriteLine("Нажмите ENTER для завершения игры.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                // RESTART: restart the game
                else if (line[0] == "restart")
                {
                    Init();
                }
            }

            Console.WriteLine("Вы проиграли и пали в забвение.");
        }

        private static void Init()
        {
            _turnsLeft = 40;

            while (true)
            {
                int choice;

                Console.WriteLine("Выберите размер поля:\n1 — 4x4\n2 — 7x7\n3 — 10x10");

                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice < 4)
                {
                    _tableLen = (choice - 1) * 3 + 4;

                    // Creating the field
                    _gameField = new Room[_tableLen, _tableLen];
                    for (int i = 0; i < _tableLen; ++i)
                    {
                        for (int q = 0; q < _tableLen; ++q)
                        {
                            _gameField[i, q] = new Room();
                        }
                    }

                    // Populating the field with keys
                    _gameField[r.Next(_tableLen), r.Next(_tableLen)].Items.Add(new RealKey());
                    _gameField[r.Next(_tableLen), r.Next(_tableLen)].Items.Add(new FakeKey());

                    // Populating the field with doors
                    _gameField[r.Next(_tableLen), r.Next(_tableLen)].Entities.Add(new Door());

                    // Populating the field with events
                    // TODO: refactor
                    List<RoomEvent> roomEvents = new List<RoomEvent>
                    {
                        new RoomEvent("Какое число больше: 10 или 20?", new List<string>{"20"}, null, 2),
                        new RoomEvent("Что из перечисленного является цитрусом: лимон, авокадо, персик, лайм?", new List<string>{"лимон", "лайм"}, null, 5),
                        new RoomEvent("Загадка: висит груша, нельзя скушать. Что это?", new List<string>{"лампочка"}, new List<Item>{ new Shield() }, 10)
                    };

                    foreach (RoomEvent roomEvent in roomEvents)
                    {
                        _gameField[r.Next(_tableLen), r.Next(_tableLen)].Events.Add(roomEvent);
                    }

                    // Spawning the player
                    _player = new Player("Джон Доу");
                    _player.X = r.Next(_tableLen);
                    _player.Y = r.Next(_tableLen);

                    break;
                }
            }
        }

        private static Room GetCurrentRoom()
        {
            return _gameField[_player.Y, _player.X];
        }
    }
}
