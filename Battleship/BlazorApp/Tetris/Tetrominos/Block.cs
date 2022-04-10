using BlazorApp.Tetris.Enums;

namespace BlazorApp.Tetris.Tetrominos
{
    /// <summary>
    /// A square or "block" tetromino. Block tetrominos do not rotate.
    /// X X
    /// X X
    /// </summary>
    public class Block : Tetromino
    {
        public Block(int x, int y) : base(x, y) { }

        public override TetrominoStyle Style => TetrominoStyle.Block;

        public override string CssClass => "tetris-yellow-cell";

        public override CellCollection CoveredCells
        {
            get
            {
                CellCollection cells = new CellCollection();
                cells.Add(CenterPieceRow, CenterPieceColumn);
                cells.Add(CenterPieceRow - 1, CenterPieceColumn);
                cells.Add(CenterPieceRow, CenterPieceColumn + 1);
                cells.Add(CenterPieceRow - 1, CenterPieceColumn + 1);
                return cells;
            }
        }
    }
}
