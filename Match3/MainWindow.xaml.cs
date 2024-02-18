using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Match3.Logic;
using Match3.Models;
using Match3.Views;

namespace Match3;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Constants

    private const int AnimationStepDurationMs = 10;

    private const double AnimationStep = 0.15;

    #endregion


    #region Fields

    private readonly DispatcherTimer mAnimationTimer = new();
    
    private readonly EmptyCellsChecker mEmptyCellsChecker = new();

    private readonly MatchChecker mMatchChecker = new();
    
    private BallVm? mSelectedBall;

    #endregion


    #region Model dependency property

    public static readonly DependencyProperty ModelProperty = DependencyProperty.
        Register(nameof(Model), typeof(GameFieldVm), typeof(MainWindow),
        new PropertyMetadata(null));


    public GameFieldVm Model
    {
        get => (GameFieldVm) GetValue(ModelProperty);
        private set => SetValue(ModelProperty, value);
    }

    #endregion


    #region Init and clean-up

    public MainWindow()
    {
        Model = new GameFieldVm();
        InitializeComponent();
        mAnimationTimer.Interval = TimeSpan.FromMilliseconds(AnimationStepDurationMs);
        mAnimationTimer.Tick += AnimationTimerTick;
        NewGameButtonClick(this, default);
    }

    #endregion


    #region UI handlers

    private void NewGameButtonClick(object sender, RoutedEventArgs? e)
    {
        mAnimationTimer.Stop();
        Model.NewGame();
        InitiateAction(GameStateActions.CheckEmptyCells);
    }


    private void FieldClickedHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var ball = Field.GetObjectAtPoint<ContentPresenter, BallVm>(e.GetPosition(Field));
        if (ball is null) return;

        if (Model.State == GameStates.Idle)
        {
            ball.IsSelected = true;
            mSelectedBall = ball;
            Model.State = GameStates.Selected;
            return;
        }

        if (Model.State == GameStates.Selected)
        {
            if (ball.IsSelected)
            {
                ball.IsSelected = false;
                mSelectedBall = null;
                Model.State = GameStates.Idle;
                return;
            }

            mSelectedBall!.IsSelected = false;
            var left = Model.GetBall(ball.Coordinate.X - 1, ball.Coordinate.Y) == mSelectedBall;
            var right = Model.GetBall(ball.Coordinate.X + 1, ball.Coordinate.Y) == mSelectedBall;
            var above = Model.GetBall(ball.Coordinate.X, ball.Coordinate.Y - 1) == mSelectedBall;
            var below = Model.GetBall(ball.Coordinate.X, ball.Coordinate.Y + 1) == mSelectedBall;

            if (left || right || above || below)
            {
                Model.Swap(ball, mSelectedBall!);
                Model.State = GameStates.Swapped;
                InitiateAction(GameStateActions.CheckMatches);
                mSelectedBall = null;
            }
            else
            {
                ball.IsSelected = true;
                mSelectedBall = ball;
            }
        }
    }

    #endregion


    #region States

    private void InitiateAction(GameStateActions action) =>
        Dispatcher.Invoke(() => InitiateActionUnsafe(action), DispatcherPriority.ApplicationIdle);


    private void InitiateActionUnsafe(GameStateActions action)
    {
        mAnimationTimer.Stop();

        switch (Model.State)
        {
            case GameStates.Idle:
                switch (action)
                {
                    case GameStateActions.CheckEmptyCells:
                        Model.State = GameStates.Falling;

                        if (mEmptyCellsChecker.CheckEmptyCells(Model.Cells))
                            mAnimationTimer.Start();
                        else
                            InitiateAction(GameStateActions.CheckMatches);
                        break;
                }
                break;

            case GameStates.Falling:
            case GameStates.Swapped:
                switch (action)
                {
                    case GameStateActions.CheckMatches:
                        var deleted = mMatchChecker.CheckMatches(Model.Cells);
                        if (deleted > 0)
                        {
                            Model.State = GameStates.Idle;
                            Model.Score += deleted;
                            InitiateAction(GameStateActions.CheckEmptyCells);
                        }
                        else
                        {
                            Model.State = GameStates.Idle;
                        }

                        break;
                }
                break;
        }
    }


    private void AnimationTimerTick(object? sender, EventArgs e)
    {
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
            InitiateAction(GameStateActions.CheckMatches);
        }
    }

    #endregion
}
