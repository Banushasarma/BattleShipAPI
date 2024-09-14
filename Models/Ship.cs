namespace BattleShipAPI.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public List<(int row, int col)> Coordinates { get; set; }
        public int Size { get; set; }
        public int Hits { get; set; }

        public Ship(string name, int size)
        {
            Name = name;
            Size = size;
            Coordinates = new List<(int row, int col)>();
            Hits = 0;
        }

        public bool IsSunk()
        {
            return Hits >= Size;
        }

        public void Hit()
        {
            Hits++;
        }
    }
}
