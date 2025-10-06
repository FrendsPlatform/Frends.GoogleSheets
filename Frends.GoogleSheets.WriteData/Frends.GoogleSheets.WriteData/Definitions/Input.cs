using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.GoogleSheets.WriteData.Definitions;

/// <summary>
/// Essential parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// The spreadsheet ID of the Google Sheets document.
    /// </summary>
    /// <example>abcdef12345...98765ABC</example>
    public string SpreadsheetId { get; set; }

    /// <summary>
    /// The starting cell to write data to, in A1 notation.
    /// </summary>
    /// <example>Sheet1!A1:C10</example>
    [DefaultValue("Sheet1!A1")]
    public string StartingCell { get; set; }

    /// <summary>
    /// The values to write, formatted as a JSON string representing a 2D array.
    /// Each inner array represents a row of values.
    /// </summary>
    /// <example>[ ["Value1", "Value2"], ["Value3", "Value4"] ]</example>
    public string Values { get; set; }
}