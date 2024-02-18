using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Match3.Views;


public sealed class BallView : Border
{
    #region BaseColor dependency property

    public static readonly DependencyProperty BaseColorProperty = DependencyProperty.
        Register(nameof(BaseColor), typeof(Color), typeof(BallView));


    public Color BaseColor
    {
        get => (Color) GetValue(BaseColorProperty);
        set => SetValue(BaseColorProperty, value);
    }

    #endregion


    #region TopMargin dependency property

    public static readonly DependencyProperty TopMarginProperty = DependencyProperty.
        Register(nameof(TopMargin), typeof(double), typeof(BallView));


    public double TopMargin
    {
        get => (double) GetValue(TopMarginProperty);
        set => SetValue(TopMarginProperty, value);
    }

    #endregion
}
