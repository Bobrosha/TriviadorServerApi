using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TriviadorServerApi.Entities
{
    public class TriviadorMap
    {
        public List<Cell> Cells { get; set; }
        public List<Player> Players { get; set; }

        public class Cell
        {
            public int Id { get; set; }
            public int Value { get; set; }
            public Player Owner { get; set; }
            public int Lvl { get; set; }
            public List<int> NearestCells { get; set; }
            public Castle Castle { get; set; }

            [JsonConstructor]
            public Cell(int id, int value, Player owner, int lvl, List<int> nearestCells, Castle castle)
            {
                Id = id;
                Value = value;
                Owner = owner;
                Lvl = lvl;
                NearestCells = nearestCells;
                Castle = castle;
            }

            public Cell(int id, int value, int[] nearestCells, int lvl = 0)
            {
                Id = id;
                Value = value;
                Lvl = lvl;
                NearestCells = new List<int>();
                foreach (var el in nearestCells)
                {
                    NearestCells.Add(el);
                }

                Owner = null;
                Castle = null;
            }

            public void SetCastle(Castle castle)
            {
                Castle = castle;
                Value += castle.Value;
            }
        }

        public class Castle
        {
            public int Id { get; set; }
            public Player Owner { get; set; }
            public int Lvl { get; set; }
            public int Hp { get; set; }
            public int Value { get; set; }

            [JsonConstructor]
            public Castle(int id, Player owner, int value, int hp = 1000, int lvl = 0)
            {
                Id = id;
                Owner = owner;
                Value = value;
                Hp = hp;
                Lvl = lvl;
            }
        }

        public TriviadorMap()
        {
            Cells = CreateCellsList();
            Players = new List<Player>();
        }

        [JsonConstructor]
        public TriviadorMap(List<Cell> cells, List<Player> players)
        {
            Cells = cells;
            Players = players;
        }

        public static List<Cell> CreateCellsList()
        {
            var list = new List<Cell>();

            CreateCellAndAddInList(list, 1, new int[] { 2, 3, 4 });
            CreateCellAndAddInList(list, 2, new int[] { 1, 3 });
            CreateCellAndAddInList(list, 3, new int[] { 1, 2, 4 });
            CreateCellAndAddInList(list, 4, new int[] { 1, 3, 6, 5 });
            CreateCellAndAddInList(list, 5, new int[] { 4, 6, 7, 8 });
            CreateCellAndAddInList(list, 6, new int[] { 1, 4, 5, 7, 8 });
            CreateCellAndAddInList(list, 7, new int[] { 5, 6, 8, 9, 10, 11 });
            CreateCellAndAddInList(list, 8, new int[] { 5, 6, 7, 11 });
            CreateCellAndAddInList(list, 9, new int[] { 7, 10, 13 });
            CreateCellAndAddInList(list, 10, new int[] { 7, 9, 11, 12, 13, 14 });
            CreateCellAndAddInList(list, 11, new int[] { 7, 8, 10, 12 });
            CreateCellAndAddInList(list, 12, new int[] { 10, 11, 14 });
            CreateCellAndAddInList(list, 13, new int[] { 9, 10, 14 });
            CreateCellAndAddInList(list, 14, new int[] { 10, 12, 13 });
            CreateCellAndAddInList(list, 15, new int[] { 2, 3 });

            return list;
        }

        public static void CreateCellAndAddInList(List<Cell> list, int id, int[] arr, int score = 200)
        {
            list.Add(new Cell(id, score, arr));
        }
    }
}
