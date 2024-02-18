namespace Match3.Logic;


public record struct CellMatchInfo(
    int MatchedX, int MatchedY,
    int XBall, int XLeft, int XRight,
    int YBall, int YUp, int YDown);
