namespace Domain.Model.Api
{
    public class GameViewDTO
    {
        public GameDataSerializable GameData { get; set; }
        public string Base64Picture { get; set; }
        public string ShipPlacementStatus { get; set; }

        public GameViewDTO(GameDataSerializable gameData, string base64Picture, string shipPlacementStatus)
        {
            GameData = gameData;
            Base64Picture = base64Picture;
            ShipPlacementStatus = shipPlacementStatus;
        }
            
        
    }
}