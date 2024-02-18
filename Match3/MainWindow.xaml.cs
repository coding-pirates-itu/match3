using System.Windows;
using System.Windows.Threading;
using Match3.Logic;
using Match3.Models;

namespace Match3;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Constants

    private const int AnimationStepDurationMs = 50;

    private const double AnimationStep = 0.1;

    #endregion


    #region Fields

    private EmptyCellsChecker mEmptyCellsChecker = new();

    private DispatcherTimer mAnimationTimer = new DispatcherTimer();
    
    #endregion


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
        Model = new GameFieldVm();
        InitializeComponent();
        mAnimationTimer.Interval = TimeSpan.FromMilliseconds(AnimationStepDurationMs);
        mAnimationTimer.Tick += AnimationTimerTick;
        Dispatcher.BeginInvoke(() => NewGameButtonClick(this, default), DispatcherPriority.ApplicationIdle);
    }

    #endregion


    #region UI handlers

    private void NewGameButtonClick(object sender, RoutedEventArgs? e)
    {
        mAnimationTimer.Stop();
        Model.NewGame();
        
        if (mEmptyCellsChecker.CheckEmptyCells(Model.Cells))
        {
            Model.State = GameStates.Falling;
            mAnimationTimer.Start();
        }
        else
        {
            Model.State = GameStates.Idle;
        }

        Field.InvalidateVisual();
    }

    #endregion


    #region Timer

    private void AnimationTimerTick(object? sender, EventArgs e)
    {
        if (Model.State != GameStates.Falling)
        {
            mAnimationTimer.Stop();
            return;
        }

        var changed = false;

        foreach (var cell in Model.Cells)
        {
            if (cell.Displacement < 0)
            {
                cell.Displacement += AnimationStep;
                if (cell.Displacement > 0) cell.Displacement = 0;
                changed = true;
            }
        }

        if (!changed)
        {
            Model.State = GameStates.Idle;
        }
    }

    #endregion
}
