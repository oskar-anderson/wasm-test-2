namespace Domain.Model
{
    public struct GameResult
    {
        public readonly bool IsOver;
        public readonly GameData Data;

        public GameResult(bool isOver, GameData data)
        {
            IsOver = isOver;
            Data = data;
        }
    }
}