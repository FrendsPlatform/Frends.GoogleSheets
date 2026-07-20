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
    /// <example>[[\"Value1\", \"Value2\"],[\"Value3\", \"Value4\"]]</example>
    public dynamic Data { get; set; }

    /// <summary>
    /// Range that was read from the sheet.
    /// </summary>
    /// <example>Sheet1!A1:B2</example>
    public string Range { get; set; }

    /// <summary>
    /// Major dimension of the returned data (ROWS or COLUMNS).
    /// </summary>
    /// <example>ROWS</example>
    public string MajorDimension { get; set; }

    /// <summary>
    /// ETag of the response.
    /// </summary>
    /// <example>\"someEtag\"</example>
    public string ETag { get; set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; set; }
}