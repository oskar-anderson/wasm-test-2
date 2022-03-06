using System.Collections.Generic;
using Domain.Tile;

namespace Domain.Model.Api
{
    public class GameViewDTO_v2
    {
        public GameDataSerializable GameData { get; set; }
        public TileData.CharInfo[][] Board { get; set; }
        public string ShipPlacementStatus { get; set; }

        public GameViewDTO_v2(GameDataSerializable gameData, TileData.CharInfo[][] board, string shipPlacementStatus)
        {
            GameData = gameData;
            Board = board;
            ShipPlacementStatus = shipPlacementStatus;
        }
            
        
    }
}