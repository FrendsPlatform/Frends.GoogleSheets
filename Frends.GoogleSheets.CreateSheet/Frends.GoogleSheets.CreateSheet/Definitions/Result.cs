namespace Frends.GoogleSheets.CreateSheet.Definitions;

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
    /// The ID of the newly created sheet.
    /// </summary>
    /// <example>12345</example>
    public int NewSheetId { get; set; }

    /// <summary>
    /// ETag of the response.
    /// </summary>
    public string ETag { get; set; }

    /// <summary>
    /// Error that occurred during task execution.
    /// </summary>
    /// <example>object { string Message, Exception AdditionalInfo }</example>
    public Error Error { get; set; }
}