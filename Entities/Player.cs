namespace TriviadorServerApi.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Player(string name, int score = 0)
        {
            Name = name;
            Score = score;
        }
    }
}
