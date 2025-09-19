using System.ComponentModel;

namespace Frends.GoogleSheets.CreateSheet.Definitions;

/// <summary>
/// Connection parameters.
/// </summary>
public class Connection
{
    /// <summary>
    /// JSON string containing service account credentials.
    /// </summary>
    /// <example>{ "type": "service_account", "project_id": ..., "private_key_id": ... }</example>
    [PasswordPropertyText]
    public string ServiceAccountJson { get; set; }
}