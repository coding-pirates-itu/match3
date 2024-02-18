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
    public bool CheckEmptyCells(IArray2D<BallVm> cells)
    {
        cells.StartBulk();

        for (var x = 0; x < cells.Dimension1; x++)
        {
            var nextToTakeFrom = default(int?);

            for (var y = cells.Dimension2 - 1;  y >= 0; y--)
                if (cells.Get(x, y) is null)
                {
                    var takeFrom = nextToTakeFrom ?? y - 1;
                    while (takeFrom >= 0 && cells.Get(x, takeFrom) is null)
                        takeFrom--;

                    BallVm ball;
                    if (takeFrom >= 0 && cells.Get(x, takeFrom) is not null)
                    {
                        ball = cells.Get(x, takeFrom)!;
                        ball.Coordinate = ball.Coordinate with { Y = y };
                        cells.Set(x, takeFrom, null);
                    }
                    else
                    {
                        ball = BallVm.CreateItem(x, y);
                    }

                    nextToTakeFrom = takeFrom - 1;
                    ball.Displacement = takeFrom - y;
                    cells.Set(x, y, ball);
                }
        }

        return cells.EndBulk();
    }
}
