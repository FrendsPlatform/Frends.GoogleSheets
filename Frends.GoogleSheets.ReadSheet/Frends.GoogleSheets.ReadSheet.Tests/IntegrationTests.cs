using System;
using System.Text;
using System.Threading;
using Frends.GoogleSheets.ReadSheet.Definitions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Frends.GoogleSheets.ReadSheet.Tests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public void ReadSheet_ReadsCorrectData()
    {
        var saJson = Encoding.UTF8.GetString(Convert.FromBase64String(Environment.GetEnvironmentVariable("GOOGLE_SERVICE_ACCOUNT_JSON_BASE64")));
        var spreadsheetId = Environment.GetEnvironmentVariable("GOOGLE_SHEET_ID");
        var input = new Input
        {
            SpreadsheetId = spreadsheetId,
            Range = "A1:B2",
        };

        var connection = new Connection
        {
            ServiceAccountJson = saJson,
        };

        var result = GoogleSheets.ReadSheet(input, connection, new Options(), CancellationToken.None);

        Assert.That(result.Success, Is.True, result.Error?.Message);
        Assert.That(result.Range, Is.EqualTo("Sheet1!A1:B2"));
        Assert.That(result.MajorDimension, Is.EqualTo("ROWS"));
        Assert.That(result.ETag, Is.Not.Empty);
        Assert.That(result.Data, Is.Not.Null);
        Assert.That(result.Data.Type, Is.EqualTo(JTokenType.Array));
    }
}