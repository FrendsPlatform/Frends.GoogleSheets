namespace Frends.GoogleSheets.CreateSheet.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Unable to access Google Sheets.</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error, if any.
    /// </summary>
    /// <example>object { Exception Exception }</example>
    public dynamic AdditionalInfo { get; set; }
}
