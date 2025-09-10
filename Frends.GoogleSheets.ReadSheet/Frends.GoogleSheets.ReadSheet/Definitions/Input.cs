using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.GoogleSheets.ReadSheet.Definitions;

/// <summary>
/// Essential parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// The ID of the spreadsheet.
    /// Example: "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms"
    /// </summary>
    /// <example>abcdef12345000000000000000000000000000000000</example>
    public string SpreadsheetId { get; set; }

    /// <summary>
    /// The range of cells to read from the sheet, in A1 notation.
    /// Example: "Sheet1!A1:C10"
    /// </summary>
    /// <example>Sheet1!A1:C10</example>
    [DefaultValue("Sheet1!A1:C10")]
    public string Range { get; set; }
}
