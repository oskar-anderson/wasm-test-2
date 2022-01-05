namespace Domain.Model.Api
{
    public class GameDataAndInput
    {
        public GameDataSerializable GameDataSerializable { get; set; } = null!;
        public Input Input { get; set; } = null!;
    }
}