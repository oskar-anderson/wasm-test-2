using BlazorApp.Tetris.Tetrominos;

namespace BlazorApp.Tetris;

public class GameData
{
    public Grid Grid { get; set; }

    public TetrominoGenerator Generator { get; set; }

    public Tetromino? CurrentTetromino { get; set; }

    public int Level { get; set; } = 1;
    public int Score { get; set; } = 0;
    
    public GameData()
    {
        Grid = new Grid();
        Generator = new TetrominoGenerator();
        CurrentTetromino = Generator.CreateFromStyle(Generator.Next(), x: Grid.Width / 2, y: Grid.Height);
    }
}