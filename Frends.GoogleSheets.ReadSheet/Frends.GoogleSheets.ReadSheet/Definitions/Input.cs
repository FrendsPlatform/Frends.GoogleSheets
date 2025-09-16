using System.ComponentModel;

namespace Frends.GoogleSheets.ReadSheet.Definitions;

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
    /// The range of cells to read from the sheet, in A1 notation.
    /// </summary>
    /// <example>Sheet1!A1:C10</example>
    [DefaultValue("Sheet1!A1:C10")]
    public string Range { get; set; }
}