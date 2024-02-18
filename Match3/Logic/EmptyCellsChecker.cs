using Match3.Models;

namespace Match3.Logic;


public sealed class EmptyCellsChecker
{
    /// <summary>
    /// Check the given array for empty cells.
    /// If there is something above, move it down and set <see cref="BallVm.Displacement"/> to -1.
    /// If nothing, add a new item and set <see cref="BallVm.Displacement"/> to -y.
    /// </summary>
    /// <return><see langword="true"/> if any modifications were made.</return>
    public bool CheckEmptyCells(BallVm?[,] cells)
    {
        var changed = false;

        for (var x = 0; x < cells.GetLength(0); x++)
            for (var y = cells.GetLength(1) - 1;  y >= 0; y--)
                if (cells[x, y] is null)
                {
                    var takeFrom = y - 1;
                    while (takeFrom >= 0 && cells[x, takeFrom] is null)
                        takeFrom--;

                    if (takeFrom >= 0 && cells[x, takeFrom] is not null)
                    {
                        cells[x, y] = cells[x, takeFrom];
                        cells[x, takeFrom] = null;
                        cells[x, y]!.Coordinate = cells[x, y]!.Coordinate with { Y = y };
                    }
                    else
                    {
                        cells[x, y] = BallVm.CreateItem(x, y);
                    }

                    cells[x, y]!.Displacement = takeFrom - y;
                    changed = true;
                }

        return changed;
    }
}
