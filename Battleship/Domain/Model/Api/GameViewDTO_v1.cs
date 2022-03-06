using System.Collections.Generic;

namespace Domain.Model.Api
{
    public class GameViewDTO_v1
    {
        public GameDataSerializable GameData { get; set; }
        public List<byte> Board { get; set; }
        public string ShipPlacementStatus { get; set; }

        public GameViewDTO_v1(GameDataSerializable gameData, List<byte> board, string shipPlacementStatus)
        {
            GameData = gameData;
            Board = board;
            ShipPlacementStatus = shipPlacementStatus;
        }
            
        
    }
}