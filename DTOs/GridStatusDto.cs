namespace BattleShipAPI.DTOs
{
    public class GridStatusDto
    {
        public string[,] Board { get; set; }
        public List<string> RemainingShips { get; set; }
    }
}
