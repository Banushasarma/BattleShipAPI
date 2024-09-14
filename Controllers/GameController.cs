using BattleShipAPI.DTOs;
using BattleShipAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static Grid gameGrid = new Grid();

        [HttpPost("fire")]
        public IActionResult Fire([FromBody] string position)
        {
            var result = gameGrid.FireShot(position.ToUpper());
            if (gameGrid.AllShipsSunk())
            {
                return Ok(new { result, message = "All ships sunk! You won!" });
            }

            return Ok(new { result });
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var remainingShips = gameGrid.Ships
                .Where(ship => !ship.IsSunk())
                .Select(ship => $"{ship.Name} ({ship.Size} squares)")
                .ToList();

            var gridStatus = new GridStatusDto
            {
                Board = gameGrid.Board,
                RemainingShips = remainingShips
            };

            return Ok(gridStatus);
        }
    }
}
