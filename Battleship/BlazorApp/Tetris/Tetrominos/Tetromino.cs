using BlazorApp.Tetris.Enums;

namespace BlazorApp.Tetris.Tetrominos
{
    public class Tetromino
    {
        /// <summary>
        /// The current orientation of this tetromino. Tetrominos rotate about their center.
        /// </summary>
        public TetrominoOrientation Orientation { get; set; } = TetrominoOrientation.LeftRight;

        /// <summary>
        /// The X-coordinate of the center piece.
        /// </summary>
        public int CenterPieceRow { get; set; }

        /// <summary>
        /// The Y-coordinate of the center piece.
        /// </summary>
        public int CenterPieceColumn { get; set; }

        /// <summary>
        /// The style of this tetromino, e.g. Straight, Block, T-Shaped, etc.
        /// </summary>
        public virtual TetrominoStyle Style { get; }

        /// <summary>
        /// The CSS class that is unique to this style of tetromino.
        /// </summary>
        public virtual string CssClass { get; } = "";

        /// <summary>
        /// A collection of all spaces currently occupied by this tetromino.
        /// This collection is calculated by each style.
        /// </summary>
        public virtual CellCollection CoveredCells { get; } = new CellCollection();

        public Tetromino(int x, int y)
        {
            CenterPieceRow = y;
            CenterPieceColumn = x;
        }

        /// <summary>
        /// Rotates the tetromino around the center piece. Tetrominos always rotate clockwise.
        /// </summary>
        public void Rotate() 
        { 
            switch(Orientation)
            {
                case TetrominoOrientation.UpDown:
                    Orientation = TetrominoOrientation.RightLeft;
                    break;

                case TetrominoOrientation.RightLeft:
                    Orientation = TetrominoOrientation.DownUp;
                    break;

                case TetrominoOrientation.DownUp:
                    Orientation = TetrominoOrientation.LeftRight;
                    break;

                case TetrominoOrientation.LeftRight:
                    Orientation = TetrominoOrientation.UpDown;
                    break;
            }

            var coveredSpaces = CoveredCells;

            //If the new rotation of the tetromino means it would be outside the
            //play area, shift the center space so as to keep the entire tetromino visible.
            if(coveredSpaces.HasColumn(-1))
            {
                CenterPieceColumn += 2;
            }
            else if (coveredSpaces.HasColumn(12))
            {
                CenterPieceColumn -= 2;
            }
            else if (coveredSpaces.HasColumn(0))
            {
                CenterPieceColumn++;
            }
            else if (coveredSpaces.HasColumn(11))
            {
                CenterPieceColumn--;
            }
        }

        /// <summary>
        /// Moves the tetromino one column to the right
        /// </summary>
        public void MoveRight(Grid grid)
        {
            if (CanMoveRight(grid))
            {
                CenterPieceColumn++;
            }
        }

        /// <summary>
        /// Make the tetromino drop all the way to its lowest possible point.
        /// </summary>
        /// <returns>The score gained from dropping the piece</returns>
        public int Drop(Grid grid)
        {
            int scoreCounter = 0;
            while(CanMoveDown(grid))
            {
                MoveDown(grid);
                scoreCounter++;
            }

            return scoreCounter;
        }

        /// <summary>
        /// Move the tetromino down one row.
        /// </summary>
        public void MoveDown(Grid grid)
        {
            if (CanMoveDown(grid))
            {
                CenterPieceRow--;
            }
        }

        /// <summary>
        /// Returns whether or not the tetromino can move in any direction (down, left, right).
        /// </summary>
        /// <returns></returns>
        public bool CanMove(Grid grid)
        {
            return CanMoveDown(grid) || CanMoveLeft(grid) || CanMoveRight(grid);
        }

        /// <summary>
        /// Returns whether or not the tetromino can move down.
        /// </summary>
        /// <returns></returns>
        public bool CanMoveDown(Grid grid)
        {
            //For each of the covered spaces, get the space immediately below
            foreach (var coord in CoveredCells.GetLowest())
            {
                if (grid.Cells.Contains(coord.Row - 1, coord.Column))
                    return false;
            }

            //If any of the covered spaces are currently in the lowest row, the piece cannot move down.
            if (CoveredCells.HasRow(1))
                return false;

            return true;
        }

        /// <summary>
        /// Returns whether or not the tetromino can move right
        /// </summary>
        /// <returns></returns>
        public bool CanMoveRight(Grid grid)
        {
            //For each of the covered spaces, get the space immediately to the right
            foreach (var cell in CoveredCells.GetRightmost())
            {
                if (grid.Cells.Contains(cell.Row, cell.Column + 1))
                    return false;
            }

            //If any of the covered spaces are currently in the rightmost column, the piece cannot move right.
            if (CoveredCells.HasColumn(grid.Width))
                return false;

            return true;
        }

        /// <summary>
        /// Moves the tetromino one column to the left.
        /// </summary>
        public void MoveLeft(Grid grid)
        {
            if (CanMoveLeft(grid))
            {
                CenterPieceColumn--;
            }
        }

        /// <summary>
        /// Returns whether or not the tetromino can move to the left.
        /// </summary>
        /// <returns></returns>
        public bool CanMoveLeft(Grid grid)
        {
            //For each of the covered spaces, get the space immediately to the left
            foreach (var cell in CoveredCells.GetLeftmost())
            {
                if (grid.Cells.Contains(cell.Row, cell.Column - 1))
                    return false;
            }

            //If any of the covered spaces are currently in the leftmost column, the piece cannot move left.
            if (CoveredCells.HasColumn(1))
                return false;

            return true;
        }
    }
}
