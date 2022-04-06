namespace Domain.Model.Api;

public class GameDataAndTimeDiff
{
    public GameDataSerializable GameData { get; set; }
    public double DeltaTime { get; set; }

    public GameDataAndTimeDiff(GameDataSerializable gameData, double deltaTime)
    {
        GameData = gameData;
        DeltaTime = deltaTime;
    }
}