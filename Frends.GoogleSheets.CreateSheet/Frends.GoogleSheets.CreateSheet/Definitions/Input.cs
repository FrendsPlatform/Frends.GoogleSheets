using System.ComponentModel;

namespace Frends.GoogleSheets.CreateSheet.Definitions;

/// <summary>
/// Essential parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// The ID of the spreadsheet.
    /// </summary>
    /// <example>abcdef12345...98765ABC</example>
    public string SpreadsheetId { get; set; }

    /// <summary>
    /// The name of the new sheet to be created.
    /// </summary>
    /// <example>NewSheet</example>
    [DefaultValue("NewSheet")]
    public string NewSheetName { get; set; }

    /// <summary>
    /// Whether the new sheet should be hidden.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(false)]
    public bool Hidden { get; set; } = false;
}