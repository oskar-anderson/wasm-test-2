using System.Collections.Generic;
using Domain.Model;
using Domain.Tile;

namespace WebApp.ApiControllers.Models
{
    public class GameViewDTO
    {
        public GameDataSerializable GameData { get; set; }
        public List<byte> Board { get; set; }
        public string ShipPlacementStatus { get; set; }

        public GameViewDTO(GameDataSerializable gameData, List<byte> board, string shipPlacementStatus)
        {
            GameData = gameData;
            Board = board;
            ShipPlacementStatus = shipPlacementStatus;
        }
            
        
    }
}