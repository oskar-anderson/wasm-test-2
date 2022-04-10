using BlazorApp.Tetris.Enums;
using BlazorApp.Tetris.Tetrominos;

namespace BlazorApp.Tetris;

public class TetrominoGenerator
{
    public TetrominoStyle Next(params TetrominoStyle[] unusableStyles)
    {
        Random rand = new Random(DateTime.Now.Millisecond);

        //Randomly generate one of the eight possible tetrominos
        var style = (TetrominoStyle)rand.Next(0, 7);

        //Re-generate the new tetromino until it is of a style that is not one of the upcoming styles.
        while (unusableStyles.Contains(style))
            style = (TetrominoStyle)rand.Next(0, 7);

        return style;
    }

    public Tetromino CreateFromStyle(TetrominoStyle style, int x, int y)
    {
        return style switch
        {
            TetrominoStyle.Block => new Block(x, y),
            TetrominoStyle.Straight => new Straight(x, y),
            TetrominoStyle.TShaped => new TShaped(x, y),
            TetrominoStyle.LeftZigZag => new LeftZigZag(x, y),
            TetrominoStyle.RightZigZag => new RightZigZag(x, y),
            TetrominoStyle.LShaped => new LShaped(x, y),
            TetrominoStyle.ReverseLShaped => new ReverseLShaped(x, y),
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };
    }
}