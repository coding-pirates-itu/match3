using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Match3.Models;


/// <summary>
/// Notifying collection of game balls.
/// </summary>
public sealed class BallCollection : DependencyObject, IEnumerable<BallVm>, INotifyCollectionChanged, IArray2D<BallVm>
{
    #region Events

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    #endregion


    #region Fields

    private readonly BallVm?[,] mItems;

    private int mBulkLevel;

    private bool mBulkChanged;

    #endregion


    #region Properties

    public int Width { get; }

    public int Height { get; }

    int IArray2D<BallVm>.Dimension1 => Width;
    
    int IArray2D<BallVm>.Dimension2 => Height;

    #endregion


    #region Init and clean-up

    public BallCollection(int width, int height)
    {
        Width = width;
        Height = height;
        mItems = new BallVm[width, height];
    }

    #endregion


    #region API

    public static BallCollection Create(int width, int height) => new(width, height);


    /// <inheritdoc/>
    BallVm? IArray2D<BallVm>.Get(int coordinate1, int coordinate2) => mItems[coordinate1, coordinate2];


    /// <inheritdoc/>
    bool IArray2D<BallVm>.Set(int coordinate1, int coordinate2, BallVm? item)
    {
        var oldItem = mItems[coordinate1, coordinate2];

        if (oldItem != item)
        {
            mItems[coordinate1, coordinate2] = item;
            
            if (mBulkLevel > 0)
            {
                mBulkChanged = true;
            }

            return true;
        }

        return false;
    }


    /// <inheritdoc/>
    void IArray2D<BallVm>.StartBulk()
    {
        mBulkLevel++;
    }


    /// <inheritdoc/>
    bool IArray2D<BallVm>.EndBulk()
    {
        mBulkLevel--;

        if (mBulkLevel == 0 && mBulkChanged)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            mBulkChanged = false;
            return true;
        }

        return false;
    }


    /// <inheritdoc/>
    IEnumerator<BallVm> IEnumerable<BallVm>.GetEnumerator()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (mItems[x, y] != null)
                    yield return mItems[x, y]!;
    }


    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<BallVm>).GetEnumerator();

    #endregion
}
