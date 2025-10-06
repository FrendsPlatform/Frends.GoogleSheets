namespace Frends.GoogleSheets.WriteData.Definitions;

/// <summary>
/// Mode for inserting data into the sheet.
/// </summary>
public enum InsertDataModes
{
    /// <summary>
    /// Data will be inserted and existing rows will be shifted down.
    /// </summary>
    InsertRows,

    /// <summary>
    /// Will overwrite existing data using append.
    /// </summary>
    AppendOverwrite,

    /// <summary>
    /// Will overwrite existing data using update.
    /// </summary>
    UpdateOverwrite,
}

/// <summary>
/// Mode for how the input data should be interpreted.
/// </summary>
public enum ValueInputModes
{
    /// <summary>
    /// The values the user has entered will be parsed as if the user typed them into the UI.
    /// </summary>
    UserEntered,

    /// <summary>
    /// The values will not be parsed and will be stored as-is.
    /// </summary>
    Raw,
}