namespace BattleShipAPI.Models
{
    public class Grid
    {
        public const int GridSize = 10;
        public string[,] Board { get; private set; }
        public List<Ship> Ships { get; private set; }

        private Random random = new Random();

        public Grid()
        {
            Board = new string[GridSize, GridSize];
            Ships = new List<Ship>();

            // Initialize grid with water
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    Board[row, col] = "~"; // ~ represents water
                }
            }

            // Place ships
            PlaceShip(new Ship("Battleship", 5));
            PlaceShip(new Ship("Destroyer", 4));
            PlaceShip(new Ship("Destroyer", 4));
        }

        private void PlaceShip(Ship ship)
        {
            bool placed = false;

            while (!placed)
            {
                int row = random.Next(GridSize);
                int col = random.Next(GridSize);
                bool horizontal = random.Next(2) == 0;

                if (CanPlaceShip(row, col, ship.Size, horizontal))
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        if (horizontal)
                        {
                            Board[row, col + i] = "S";
                            ship.Coordinates.Add((row, col + i));
                        }
                        else
                        {
                            Board[row + i, col] = "S";
                            ship.Coordinates.Add((row + i, col));
                        }
                    }
                    Ships.Add(ship);
                    placed = true;
                }
            }
        }

        private bool CanPlaceShip(int row, int col, int size, bool horizontal)
        {
            if (horizontal)
            {
                if (col + size > GridSize) return false;
                for (int i = 0; i < size; i++)
                {
                    if (Board[row, col + i] == "S") return false;
                }
            }
            else
            {
                if (row + size > GridSize) return false;
                for (int i = 0; i < size; i++)
                {
                    if (Board[row + i, col] == "S") return false;
                }
            }

            return true;
        }

        public string FireShot(string position)
        {
            int row = position[0] - 'A';
            int col = int.Parse(position[1..]) - 1;

            if (Board[row, col] == "S")
            {
                Board[row, col] = "X"; // Hit
                foreach (var ship in Ships)
                {
                    if (ship.Coordinates.Contains((row, col)))
                    {
                        ship.Hit();
                        if (ship.IsSunk())
                        {
                            return $"Hit! You sank the {ship.Name}!";
                        }
                        return "Hit!";
                    }
                }
            }
            else if (Board[row, col] == "~")
            {
                Board[row, col] = "O"; // Miss
                return "Miss!";
            }

            return "Already shot here!";
        }

        public bool AllShipsSunk()
        {
            return Ships.All(ship => ship.IsSunk());
        }
    }
}
