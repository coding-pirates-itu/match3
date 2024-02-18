using System.Windows;
using System.Windows.Input;

namespace Match3.Models;


public sealed class BallVm : DependencyObject
{
    #region Type dependency property

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type), typeof(BallTypes), typeof(BallVm),
        new PropertyMetadata(default(BallTypes)));


    public BallTypes Type
    {
        get => (BallTypes) GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    #endregion


    #region Coordinate dependency property

    public static readonly DependencyProperty CoordinateProperty = DependencyProperty.Register(
        nameof(Coordinate), typeof(Point), typeof(BallVm),
        new PropertyMetadata(default(Point)));


    public Point Coordinate
    {
        get => (Point) GetValue(CoordinateProperty);
        set => SetValue(CoordinateProperty, value);
    }

    #endregion


    #region Displacement dependency property

    public static readonly DependencyProperty DisplacementProperty = DependencyProperty.Register(
        nameof(Displacement), typeof(double), typeof(BallVm),
        new PropertyMetadata(default(double)));


    public double Displacement
    {
        get => (double) GetValue(DisplacementProperty);
        set => SetValue(DisplacementProperty, value);
    }

    #endregion


    #region IsSelected dependency property

    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected), typeof(bool), typeof(BallVm),
        new PropertyMetadata(default(bool)));


    public bool IsSelected
    {
        get => (bool) GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    #endregion


    #region API

    public static BallVm CreateItem(int x, int y)
    {
        var type = (BallTypes) Random.Shared.Next((int)BallTypes.Ball1, (int)BallTypes.Ball5 + 1);

        return new BallVm() { Coordinate = new Point(x, y), Type = type };
    }

    #endregion
}
