using System.Windows;
using System.Windows.Controls;
using Match3.Models;

namespace Match3.Views;


public sealed class GameFieldView : ItemsControl
{
    #region FieldWidth dependency property

    public static readonly DependencyProperty FieldWidthProperty = DependencyProperty.
        Register(nameof(FieldWidth), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(SizeParameterChanged));


    public int FieldWidth
    {
        get => (int) GetValue(FieldWidthProperty);
        set => SetValue(FieldWidthProperty, value);
    }

    #endregion


    #region FieldHeight dependency property

    public static readonly DependencyProperty FieldHeightProperty = DependencyProperty.
        Register(nameof(FieldHeight), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(SizeParameterChanged));


    public int FieldHeight
    {
        get => (int) GetValue(FieldHeightProperty);
        set => SetValue(FieldHeightProperty, value);
    }

    #endregion


    #region CellSize dependency property

    public static readonly DependencyProperty CellSizeProperty = DependencyProperty.
        Register(nameof(CellSize), typeof(int), typeof(GameFieldView),
        new PropertyMetadata(40));


    public int CellSize
    {
        get => (int) GetValue(CellSizeProperty);
        set => SetValue(CellSizeProperty, value);
    }

    #endregion


    #region Init and clean-up

    public GameFieldView()
    {
        SizeParameterChanged(this, default);
    }

    #endregion


    #region Utility

    private static void SizeParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var self = (GameFieldView)d;
        self.Width = self.CellSize * self.FieldWidth;
        self.Height = self.CellSize * self.FieldHeight;
    }

    #endregion
}
