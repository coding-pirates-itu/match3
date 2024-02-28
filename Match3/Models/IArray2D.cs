namespace Match3.Models;


/// <summary>
/// A two-dimensional array.
/// </summary>
public interface IArray2D<T>
{
    /// <summary>
    /// The number of elements in dimension 1.
    /// </summary>
    int Dimension1 { get; }


    /// <summary>
    /// The number of elements in dimension 2.
    /// </summary>
    int Dimension2 { get; }


    /// <summary>
    /// Get an element by coordinates.
    /// </summary>
    T? Get(int coordinate1, int coordinate2);


    /// <summary>
    /// Set an element by coordinates.
    /// </summary>
    /// <returns><see langword="true"/> if there was a change.</returns>
    bool Set(int coordinate1, int coordinate2, T? item);


    /// <summary>
    /// Start a bulk of Set operations.
    /// </summary>
    void StartBulk();


    /// <summary>
    /// End the bulk started with <see cref="StartBulk"/>.
    /// </summary>
    /// <returns><see langword="true"/> if anything was changed.</returns>
    bool EndBulk();
}
