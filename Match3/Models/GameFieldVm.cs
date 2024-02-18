using System.Windows;

namespace Match3.Models;


public sealed class GameFieldVm : DependencyObject
{
    #region Width dependency property

    public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
        nameof(Width), typeof(int), typeof(GameFieldVm),
        new PropertyMetadata(12));


    public int Width
    {
        get => (int) GetValue(WidthProperty);
        private set => SetValue(WidthProperty, value);
    }

    #endregion


    #region Height dependency property

    public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
        nameof(Height), typeof(int), typeof(GameFieldVm),
        new PropertyMetadata(15));


    public int Height
    {
        get => (int) GetValue(HeightProperty);
        private set => SetValue(HeightProperty, value);
    }

    #endregion


    #region Cells dependency property

    public static readonly DependencyProperty CellsProperty = DependencyProperty.Register(
        nameof(Cells), typeof(BallCollection), typeof(GameFieldVm));


    public BallCollection Cells
    {
        get => (BallCollection) GetValue(CellsProperty);
        private set => SetValue(CellsProperty, value);
    }

    #endregion


    #region State dependency property

    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        nameof(State), typeof(GameStates), typeof(GameFieldVm),
        new PropertyMetadata(GameStates.Idle));


    public GameStates State
    {
        get => (GameStates) GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    #endregion


    #region Score dependency property

    public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register(
        nameof(Score), typeof(int), typeof(GameFieldVm),
        new PropertyMetadata(0));


    public int Score
    {
        get => (int) GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }

    #endregion


    #region Init and clean-up

    public GameFieldVm()
    {
        Cells = new(Width, Height);
    }

    #endregion


    #region API

    /// <summary>
    /// Start a new game.
    /// </summary>
    public void NewGame()
    {
        Score = 0;
        State = GameStates.Idle;
        Cells = BallCollection.Create(Width, Height);
    }


    public BallVm? GetBall(int x, int y) =>
        x < 0 ? null :
        x >= Width ? null :
        y < 0 ? null :
        y >= Height ? null :
        (Cells as IArray2D<BallVm>).Get(x, y);


    public void Swap(BallVm ball1, BallVm ball2)
    {
        var c1 = ball2.Coordinate;
        var c2 = ball1.Coordinate;
        ball1.Coordinate = c1;
        ball2.Coordinate = c2;

        (Cells as IArray2D<BallVm>).Set(c1.X, c1.Y, ball1);
        (Cells as IArray2D<BallVm>).Set(c2.X, c2.Y, ball2);
    }

    #endregion
}
