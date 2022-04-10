using BlazorApp.Tetris.Tetrominos;

namespace BlazorApp.Tetris;

public class CellCollection
{
    public readonly List<Cell> Cells = new List<Cell>();
    
    
    public List<Cell> GetAllInRow(int row)
    {
        return Cells.Where(x => x.Row == row).ToList();
    }
    
    //Add a new cell to the collection
    public void Add(int row, int column)
    {
        Cells.Add(new Cell(row, column));
    }
    
    //Adds several new cells, each with the given CSS class
    public void AddTetromino(Tetromino tetromino)
    {
        foreach(var cell in tetromino.CoveredCells.Cells)
        {
            Cells.Add(new Cell(cell.Row, cell.Column, tetromino.CssClass));
        }
    }
    
    // Gets the rightmost (highest Column value) cell in the collection.
    public List<Cell> GetRightmost()
    {
        return Cells.Where(cell => !Contains(cell.Row, cell.Column + 1)).ToList();
    }

    // Gets the leftmost (lowest Column value) cell in the collection.
    public List<Cell> GetLeftmost()
    {
        return Cells.Where(cell => !Contains(cell.Row, cell.Column - 1)).ToList();
    }
    
    // Gets the lowest (lowest Row value) cell in the collection. 
    public List<Cell> GetLowest()
    {
        return Cells.Where(cell => !Contains(cell.Row - 1, cell.Column)).ToList();
    }
    
    public string GetCssClass(int row, int column)
    {
        var matching = Cells.FirstOrDefault(x => x.Row == row && x.Column == column);

        return matching != null ? matching.CssClass : "";
    }
    
    //Adds a CSS class to every cell in a given row
    public void SetCssClass(int row, string cssClass)
    {
        Cells.Where(x => x.Row == row)
            .ToList()
            .ForEach(x => x.CssClass = cssClass);
    }
    
    //Moves all "higher" cells down to fill in the specified completed rows.
    public void CollapseRows(List<int> rows)
    {
        //Get all cells in the completed rows
        var selectedCells = Cells.Where(x => rows.Contains(x.Row));
        
        //Add those cells to a temporary collection
        List<Cell> toRemove = new List<Cell>();
        foreach (var cell in selectedCells)
        {
            toRemove.Add(cell);
        }

        //Remove all cells in the temporary collection 
        //from the real collection.
        Cells.RemoveAll(x => toRemove.Contains(x));

        //"Collapse" the rows above the complete rows by moving them down.
        foreach (var cell in Cells)
        {
            int numberOfLessRows = rows.Count(x => x <= cell.Row);
            cell.Row -= numberOfLessRows;
        }
    }
    
    //Checks if there are any occupied cells in the given row
    public bool HasRow(int row)
    {
        return Cells.Any(c => c.Row == row);
    }
    
    //Checks if there are any occupied cells in the given column
    public bool HasColumn(int column)
    {
        return Cells.Any(c => c.Column == column);
    }
    
    //Checks if there is an occupied cell at the given coordinates.
    public bool Contains(int row, int column)
    {
        return Cells.Any(c => c.Row == row && c.Column == column);
    }
}