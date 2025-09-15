using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleSheets.WriteData.Definitions;
using NUnit.Framework;

namespace Frends.GoogleSheets.WriteData.Tests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public async Task WriteData_AppendOverwrite_Succeeds()
    {
        var saJson = Encoding.UTF8.GetString(Convert.FromBase64String(Environment.GetEnvironmentVariable("GOOGLE_SERVICE_ACCOUNT_JSON_BASE64")));
        var spWriteDataId = Environment.GetEnvironmentVariable("GOOGLE_SHEET_ID");
        var input = new Input
        {
            SpreadsheetId = spWriteDataId,
            StartingCell = "Sheet2!B2",
            Values = "[ [\"TestY\", \"Test2\"], [\"Test3\", \"Test4\"] ]",
        };

        var options = new Options
        {
            InsertDataMode = InsertDataModes.AppendOverwrite,
            ValueInputMode = ValueInputModes.UserEntered,
            ThrowErrorOnFailure = true,
        };

        var connection = new Connection
        {
            ServiceAccountJson = saJson,
        };

        var result = await GoogleSheets.WriteData(input, connection, options, CancellationToken.None);

        Assert.That(result.Success, Is.True, result.Error?.Message);
        Assert.That(result.ETag, Is.Not.Empty);
    }

    [Test]
    public async Task WriteData_UpdateOverwrite_Succeeds()
    {
        var saJson = Encoding.UTF8.GetString(Convert.FromBase64String(Environment.GetEnvironmentVariable("GOOGLE_SERVICE_ACCOUNT_JSON_BASE64")));
        var spWriteDataId = Environment.GetEnvironmentVariable("GOOGLE_SHEET_ID");
        var input = new Input
        {
            SpreadsheetId = spWriteDataId,
            StartingCell = "Sheet2!B2",
            Values = "[ [\"TestZAA\", \"Test2\"], [\"Test3\", \"Test4\"] ]",
        };

        var options = new Options
        {
            InsertDataMode = InsertDataModes.UpdateOverwrite,
            ValueInputMode = ValueInputModes.UserEntered,
            ThrowErrorOnFailure = true,
        };

        var connection = new Connection
        {
            ServiceAccountJson = saJson,
        };

        var result = await GoogleSheets.WriteData(input, connection, options, CancellationToken.None);

        Assert.That(result.Success, Is.True, result.Error?.Message);
        Assert.That(result.ETag, Is.Not.Empty);
    }
}