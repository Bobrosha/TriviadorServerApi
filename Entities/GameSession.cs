using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System;
using System.Drawing;

namespace TriviadorServerApi.Entities
{
    public class GameSession
    {
        private static TriviadorMap _Map;
        private static int _Turn;
        private static List<Question> _Questions;

        public static void Initialize()
        {
            _Map = new TriviadorMap();
            _Questions = new List<Question>();
            GetQuestions();
        }

        private static void GetQuestions()
        {
            string[] input = { "988", "1011", "983", "995" };
            _Questions.Add(new Question("В каком году было крещение Руси?", new List<string>(input)));
        }

        public static Question GetRandomQuestion()
        {
            int randomIndex = new Random().Next(0, _Questions.Count);
            return _Questions[randomIndex];
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
            var id = _Map.Players.Count;
            player.Id = id;
            player.ColorName = Enum.GetValues(typeof(KnownColor)).GetValue(id).ToString();
            _Map.Players.Add(player);
            _Turn = id;
        }

        public static List<Player> GetPlayersList()
        {
            return _Map.Players;
        }

        public static bool GetReadyStatus()
        {
            return _Map.Players.Count > 1;
        }

        public static int GetWhoseTurn()
        {
            return _Turn;
        }

        public static int? NextTurn()
        {
            if (GetReadyStatus())
            {
                LinkedList<int> linkedListNames = new(from player in _Map.Players
                                                    select player.Id);

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
            if (GetReadyStatus())
                _Turn = _Map.Players.First().Id;
        }
    }
}
