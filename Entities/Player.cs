using System.Drawing;

namespace TriviadorServerApi.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string ColorName { get; set; }
        public Player(int id, string name, string colorName, int score = 0)
        {
            Id = id;
            Name = name;
            Score = score;
            ColorName = colorName;
        }
    }
}
