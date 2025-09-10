namespace Frends.GoogleSheets.ReadSheet.Definitions;

/// <summary>
/// Result of the task.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if the task completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Sheet cell data read from the specified range.
    /// </summary>
    public dynamic Data { get; set; }

    /// <summary>
    /// Range that was read from the sheet.
    /// </summary>
    public string Range { get; set; }

    /// <summary>
    /// Major dimension of the returned data (ROWS or COLUMNS).
    /// </summary>
    public string MajorDimension { get; set; }

    /// <summary>
    /// ETag of the response.
    /// </summary>
    public string ETag { get; set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, object { Exception Exception } AdditionalInfo }</example>
    public Error Error { get; set; }
}
