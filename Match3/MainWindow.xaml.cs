using System.Windows;
using Match3.Models;

namespace Match3;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Model dependency property

    public static readonly DependencyProperty ModelProperty = DependencyProperty.
        Register(nameof(Model), typeof(GameFieldVm), typeof(MainWindow),
        new PropertyMetadata(null));


    public GameFieldVm Model
    {
        get => (GameFieldVm) GetValue(ModelProperty);
        set => SetValue(ModelProperty, value);
    }

    #endregion


    #region Init and clean-up

    public MainWindow()
    {
        InitializeComponent();
        Model = new GameFieldVm();
    }

    #endregion
    private void NewGameButtonClick(object sender, RoutedEventArgs e)
    {
        Model.NewGame();
//            Field.InvalidateMeasure();
    }
}
