using Match3.Models;

namespace Match3.Logic;


public sealed class MatchChecker
{
    #region API

    public int CheckMatches(IArray2D<BallVm> cells)
    {
        var matches = new List<CellMatchInfo>();

        for (var x = 0; x < cells.Dimension1; x++)
            for (var y = cells.Dimension2 - 1; y >= 0; y--)
                if (cells.Get(x, y) is not null)
                {
                    matches.Add(CheckCellMatch(cells, x, y));
                }

        cells.StartBulk();
        var deleted = 0;

        foreach (var m in matches)
        {
            if (m.MatchedX >= 3)
            {
                for (var ix = m.XLeft; ix <= m.XRight; ix++)
                    if (cells.Set(ix, m.YBall, null))
                        deleted++;
            }
            if (m.MatchedY >= 3)
            {
                for (var iy = m.YUp; iy <= m.YDown; iy++)
                    if (cells.Set(m.XBall, iy, null))
                        deleted++;
            }
        }

        cells.EndBulk();

        return deleted;
    }

    #endregion


    #region Utility

    private CellMatchInfo CheckCellMatch(IArray2D<BallVm> cells, int x, int y)
    {
        var cell = cells.Get(x, y);
        if (cell == null) throw new ArgumentException("Given a null cell to check.");

        var type = cell.Type;
        var matchedX = 0;
        var matchedY = 0;

        var xl = x - 1;
        var xr = x + 1;
        var yu = y - 1;
        var yd = y + 1;
        
        while (xl >= 0 && cells.Get(xl, y)?.Type == type)
        {
            matchedX++;
            xl--;
        }
        
        while (xr < cells.Dimension1 && cells.Get(xr, y)?.Type == type)
        {
            matchedX++;
            xr++;
        }
        
        while (yu >= 0 && cells.Get(x, yu)?.Type == type)
        {
            matchedY++;
            yu--;
        }
        
        while (yd < cells.Dimension2 && cells.Get(x, yd)?.Type == type)
        {
            matchedY++;
            yd++;
        }

        return new CellMatchInfo(matchedX + 1, matchedY + 1, x, xl + 1, xr - 1, y, yu + 1, yd - 1);
    }

    #endregion
}
