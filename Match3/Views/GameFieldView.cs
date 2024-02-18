using System.Windows;
using System.Windows.Controls;
using Match3.Models;

namespace Match3.Views;


public sealed class GameFieldView : ItemsControl
{
    #region FieldWidth dependency property

    public static readonly DependencyProperty FieldWidthProperty = DependencyProperty.
        Register(nameof(FieldWidth), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(20, SizeParameterChanged));


    public int FieldWidth
    {
        get => (int) GetValue(FieldWidthProperty);
        set => SetValue(FieldWidthProperty, value);
    }

    #endregion


    #region FieldHeight dependency property

    public static readonly DependencyProperty FieldHeightProperty = DependencyProperty.
        Register(nameof(FieldHeight), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(20));


    public int FieldHeight
    {
        get => (int) GetValue(FieldHeightProperty);
        set => SetValue(FieldHeightProperty, value);
    }

    #endregion


    #region CellWidth dependency property

    public static readonly DependencyProperty CellWidthProperty = DependencyProperty.
        Register(nameof(CellWidth), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(20));


    public int CellWidth
    {
        get => (int) GetValue(CellWidthProperty);
        set => SetValue(CellWidthProperty, value);
    }

    #endregion


    #region CellHeight dependency property

    public static readonly DependencyProperty CellHeightProperty = DependencyProperty.
        Register(nameof(CellHeight), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(20));


    public int CellHeight
    {
        get => (int) GetValue(CellHeightProperty);
        set => SetValue(CellHeightProperty, value);
    }

    #endregion


    #region Init and clean-up

    public GameFieldView()
    {
        SizeParameterChanged(this, default);
    }

    #endregion


    #region Layout

    protected override Size MeasureOverride(Size constraint)
    {
        foreach (var item in Items.OfType<BallVm>())
        {
            var el = ItemContainerGenerator.ContainerFromItem(item);
            var x = (double) item.Coordinate.X * CellWidth;
            var y = (item.Coordinate.Y + item.Displacement) * CellHeight;
            el.SetValue(Canvas.LeftProperty, x);
            el.SetValue(Canvas.TopProperty, y);

            ((FrameworkElement) el).Measure(constraint);
        }

        return base.MeasureOverride(constraint);
    }

    #endregion


    #region Utility

    private static void SizeParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (GameFieldView)d;
        self.Width = self.CellWidth * self.FieldWidth;
        self.Height = self.CellHeight * self.FieldHeight;
    }

    #endregion
}
