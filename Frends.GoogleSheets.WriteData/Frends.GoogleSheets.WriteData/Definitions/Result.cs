namespace Frends.GoogleSheets.WriteData.Definitions;

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
    /// Updated range after write operation.
    /// </summary>
    /// <example>Sheet1!A1</example>
    public string UpdatedRange { get; set; }

    /// <summary>
    /// Number of updated rows after write operation.
    /// </summary>
    /// <example>5</example>
    public int UpdatedRows { get; set; }

    /// <summary>
    /// Number of updated columns after write operation.
    /// </summary>
    /// <example>5</example>
    public int UpdatedColumns { get; set; }

    /// <summary>
    /// ETag of the response.
    /// </summary>
    /// <example>abc123etag</example>
    public string ETag { get; set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; set; }
}