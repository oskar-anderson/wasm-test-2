using Domain.Model;
using Domain.Tile;

namespace WebApp.ApiControllers.Models
{
    public class GameViewDTO
    {
        public GameDataSerializable GameData { get; set; }
        public TileData.CharInfo[][] Board { get; set; }
        public string ShipPlacementStatus { get; set; }

        public GameViewDTO(GameDataSerializable gameData, TileData.CharInfo[][] board, string shipPlacementStatus)
        {
            GameData = gameData;
            Board = board;
            ShipPlacementStatus = shipPlacementStatus;
        }
            
        
    }
}