using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.GoogleSheets.WriteData.Definitions;

/// <summary>
/// Additional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Whether to throw an error on failure.
    /// </summary>
    /// <example>false</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; }

    /// <summary>
    /// Overrides the error message on failure.
    /// </summary>
    /// <example>Custom error message</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; }

    /// <summary>
    /// Mode for inserting data into the sheet.
    /// - InsertRows: Will append data after the last row with data, shifting existing rows down.
    /// - UpdateOverwrite: Will overwrite existing data.
    /// - AppendOverwrite: Will append data after the last row with data, overwriting any existing data in its way.
    /// </summary>
    /// <example>InsertRows</example>
    [DefaultValue(InsertDataModes.UpdateOverwrite)]
    public InsertDataModes InsertDataMode { get; set; }

    /// <summary>
    /// Mode for how the input data should be interpreted.
    /// - UserEntered: The values the user has entered will be parsed as if the user typed them into the UI.
    /// - Raw: The values will not be parsed and will be stored as-is.
    /// </summary>
    /// <example>UserEntered</example>
    [DefaultValue(ValueInputModes.UserEntered)]
    public ValueInputModes ValueInputMode { get; set; }
}