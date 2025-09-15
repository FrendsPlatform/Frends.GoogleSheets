using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleSheets.WriteData.Definitions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Frends.GoogleSheets.WriteData.Tests;

[TestFixture]
public class UnitTests
{
    [Test]
    public void WriteData_ThrowsException_WhenThrowErrorOnFailureIsTrue()
    {
        var input = new Input { SpreadsheetId = "invalid", StartingCell = "A1:B2" };
        var connection = new Connection { ServiceAccountJson = "{}" };
        var options = new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        Assert.ThrowsAsync<Exception>(() =>
            GoogleSheets.WriteData(input, connection, options, cancellationToken));
    }

    [Test]
    public async Task WriteData_ReturnsErrorResult_WhenThrowErrorOnFailureIsFalse()
    {
        var input = new Input { SpreadsheetId = "invalid", StartingCell = "A1:B2" };
        var connection = new Connection { ServiceAccountJson = "{}" };
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        var result = await GoogleSheets.WriteData(input, connection, options, cancellationToken);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error.Message, Contains.Substring("Custom error"));
    }
}