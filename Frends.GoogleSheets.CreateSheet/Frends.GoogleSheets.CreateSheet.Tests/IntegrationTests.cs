using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleSheets.CreateSheet.Definitions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using NUnit.Framework;

namespace Frends.GoogleSheets.CreateSheet.Tests;

[TestFixture]
public class IntegrationTests
{
    private readonly string serviceAccountJson = Encoding.UTF8.GetString(Convert.FromBase64String(Environment.GetEnvironmentVariable("GOOGLE_SERVICE_ACCOUNT_JSON_BASE64")));
    private readonly string spreadsheetId = Environment.GetEnvironmentVariable("GOOGLE_SHEET_ID");

    [Test]
    public void CreateSheet_ThrowsException_WhenThrowErrorOnFailureIsTrue()
    {
        var input = new Input { SpreadsheetId = "invalid", NewSheetName = "NewSheet" };
        var connection = new Connection { ServiceAccountJson = "{}" };
        var options = new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        Assert.ThrowsAsync<Exception>(() =>
            GoogleSheets.CreateSheet(input, connection, options, cancellationToken));
    }

    [Test]
    public async Task CreateSheet_ReturnsErrorResult_WhenThrowErrorOnFailureIsFalse()
    {
        var input = new Input { SpreadsheetId = "invalid", NewSheetName = "NewSheet" };
        var connection = new Connection { ServiceAccountJson = "{}" };
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        var result = await GoogleSheets.CreateSheet(input, connection, options, cancellationToken);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error.Message, Contains.Substring("Custom error"));
    }

    [Test]
    public async Task CreateSheet_CreatesNewSheet()
    {
        var input = new Input { SpreadsheetId = spreadsheetId, NewSheetName = $"NewSheet {Guid.NewGuid()}" };
        var connection = new Connection { ServiceAccountJson = serviceAccountJson };
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        var result = await GoogleSheets.CreateSheet(input, connection, options, cancellationToken);

        Assert.That(result.Success, Is.True);
        Assert.That(result.Error, Is.Null);
        Assert.That(result.NewSheetId, Is.GreaterThan(0));
        Assert.That(result.ETag, Is.Not.Empty);

        DeleteSheet(spreadsheetId, result.NewSheetId);
    }

    private void DeleteSheet(string spreadsheetId, int sheetId)
    {
        var saJson = serviceAccountJson;
        var credential = GoogleCredential.FromJson(saJson).CreateScoped(SheetsService.Scope.Spreadsheets);
        var service = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Frends.GoogleSheets.CreateSheet.Tests",
        });

        var requestBody = new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request>
            {
                new()
                {
                    DeleteSheet = new DeleteSheetRequest
                    {
                        SheetId = sheetId,
                    },
                },
            },
        };

        var request = service.Spreadsheets.BatchUpdate(requestBody, spreadsheetId);
        request.Execute();
    }
}
