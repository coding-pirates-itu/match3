namespace Match3.Models;


public interface IArray2D<T>
{
    int Dimension1 { get; }

    int Dimension2 { get; }


    /// <summary>
    /// Get an element by coordinates.
    /// </summary>
    T? Get(int coordinate1, int coordinate2);


    /// <summary>
    /// Set an element by coordinates.
    /// </summary>
    void Set(int coordinate1, int coordinate2, T? item);


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
