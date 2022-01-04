using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace TriviadorServerApi.Entities
{
    public class GameSession
    {
        private static TriviadorMap _Map;
        private static string _Turn;

        public static void Initialize()
        {
            _Map = new TriviadorMap();
        }

        public static string GetSerializedMap()
        {
            return JsonSerializer.Serialize(_Map);
        }

        public static TriviadorMap GetMap()
        {
            return _Map;
        }

        public static void ChangeCellInMap(TriviadorMap.Cell cell)
        {
            _Map.Cells[cell.Id - 1] = cell;
        }

        public static void AddPlayer(Player player)
        {
            _Map.Players.Add(player);
            _Turn = player.Name;
        }

        public static List<Player> GetPlayersList()
        {
            return _Map.Players;
        }

        public static bool GetReadyStatus()
        {
            return _Map.Players.Count > 1;
        }

        public static string GetWhoseTurn()
        {
            return _Turn;
        }

        public static string NextTurn()
        {
            if (GetReadyStatus())
            {
                LinkedList<string> linkedListNames = new(from player in _Map.Players
                                                    select player.Name);

                var name = linkedListNames.First;
                while (name != null)
                {
                    if (name.Value == _Turn)
                    {
                        _Turn = (name.Next ?? linkedListNames.First).Value;
                        break;
                    }
                    else
                    {
                        name = name.Next;
                    }
                }

                return _Turn;
            }
            else
            {
                return null;
            }
        }

        // Deprecated
        public static void SetMap(TriviadorMap map)
        {
            _Map = map;
            _Turn = _Map.Players.First().Name;
        }
    }
}
