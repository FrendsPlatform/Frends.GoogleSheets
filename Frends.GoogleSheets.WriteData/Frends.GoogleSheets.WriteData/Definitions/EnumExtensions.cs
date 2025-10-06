using Google.Apis.Sheets.v4;

namespace Frends.GoogleSheets.WriteData.Definitions;

/// <summary>
/// Enum extension methods.
/// </summary>
internal static class EnumExtensions
{
    /// <summary>
    /// Google API enum conversion
    /// </summary>
    /// <param name="mode">Mode to map to Google API enum</param>
    /// <returns>Google API enum value</returns>
    internal static SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum ToGoogleApiAppendValue(this InsertDataModes mode)
    {
        return mode switch
        {
            InsertDataModes.InsertRows => SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS,
            InsertDataModes.AppendOverwrite => SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.OVERWRITE,
            _ => SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS
        };
    }

    /// <summary>
    /// Google API enum conversion
    /// </summary>
    /// <param name="mode">Mode to map to Google API enum</param>
    /// <returns>Google API enum value</returns>
    internal static SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum ToGoogleApiAppendValue(this ValueInputModes mode)
    {
        return mode switch
        {
            ValueInputModes.UserEntered => SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED,
            ValueInputModes.Raw => SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW,
            _ => SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
        };
    }

    /// <summary>
    /// Google API enum conversion
    /// </summary>
    /// <param name="mode">Mode to map to Google API enum</param>
    /// <returns>Google API enum value</returns>
    internal static SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum ToGoogleApiUpdateValue(this ValueInputModes mode)
    {
        return mode switch
        {
            ValueInputModes.UserEntered => SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED,
            ValueInputModes.Raw => SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW,
            _ => SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
        };
    }
}