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
        private static int _TurnQuestion = 0;
        private static List<Question> _Questions;
        private static Question _CurrentQuestion;
        private static Random random;

        public static void Initialize()
        {
            _Map = new TriviadorMap();
            _Questions = new List<Question>();
            _Turn = 0;
            random = new Random();
            InitQuestions();
        }

        private static void InitQuestions()
        {
            string[] input = { "988", "1011", "983", "995" };
            _Questions.Add(new Question("В каком году было крещение Руси?", new List<string>(input)));
        }

        public static Question GetQuestion()
        {
            if (_CurrentQuestion == null)
            {
                GetRandomQuestion();
            }

            var answers = _CurrentQuestion.ListAnswers.ToList();

            int n = answers.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = answers[k];
                answers[k] = answers[n];
                answers[n] = value;
            }

            return new Question(_CurrentQuestion.TextQuestion, answers);
        }

        public static bool CheckQuestion(string answer)
        {
            bool flag = _CurrentQuestion.ListAnswers[0].Equals(answer);
            _TurnQuestion++;
            if (_TurnQuestion >= _Map.Players.Count)
            {
                _TurnQuestion = 0;
                _CurrentQuestion = null;
            }
            return flag;
        }

        private static void GetRandomQuestion()
        {
            int randomIndex = random.Next(0, _Questions.Count);
            _CurrentQuestion = _Questions[randomIndex];
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
            //_Turn = id;
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
                //LinkedList<int> linkedListNames = new(from player in _Map.Players
                //                                    select player.Id);

                //var name = linkedListNames.First;
                //while (name != null)
                //{
                //    if (name.Value == _Turn)
                //    {
                //        _Turn = (name.Next ?? linkedListNames.First).Value;
                //        break;
                //    }
                //    else
                //    {
                //        name = name.Next;
                //    }
                //}
                _Turn = _Turn == 0 ? 1 : 0;
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
