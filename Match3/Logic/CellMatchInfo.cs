namespace Match3.Logic;


public record struct CellMatchInfo(
    int MatchedX, int MatchedY,
    int XClick, int XLeft, int XRight,
    int YClick, int YUp, int YDown);
