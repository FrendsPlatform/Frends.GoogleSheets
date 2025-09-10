using System;
using System.Collections.Generic;
using System.Threading;
using Frends.GoogleSheets.ReadSheet.Definitions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Frends.GoogleSheets.ReadSheet.Tests;

[TestFixture]
public class UnitTests
{
    [Test]
    public void ListToJToken_ReturnsCorrectJArray()
    {
        var list = new List<IList<object>>
        {
            new List<object> { "A1", "B1" },
            new List<object> { "A2", "B2" },
        };
        var token = typeof(GoogleSheets)
            .GetMethod("ListToJToken", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static) !
            .Invoke(null, [list, CancellationToken.None]) as JArray;

        Assert.That(token, Is.Not.Null);
        Assert.That(token.Count, Is.EqualTo(2));
        Assert.That(token[0][0] !.ToString(), Is.EqualTo("A1"));
        Assert.That(token[1][1] !.ToString(), Is.EqualTo("B2"));
    }

    [Test]
    public void ReadSheet_ThrowsException_WhenThrowErrorOnFailureIsTrue()
    {
        var input = new Input { SpreadsheetId = "invalid", Range = "A1:B2" };
        var connection = new Connection { ServiceAccountJson = "{}" };
        var options = new Options { ThrowErrorOnFailure = true, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        Assert.Throws<Exception>(() =>
            GoogleSheets.ReadSheet(input, connection, options, cancellationToken));
    }

    [Test]
    public void ReadSheet_ReturnsErrorResult_WhenThrowErrorOnFailureIsFalse()
    {
        var input = new Input { SpreadsheetId = "invalid", Range = "A1:B2" };
        var connection = new Connection { ServiceAccountJson = "{}" };
        var options = new Options { ThrowErrorOnFailure = false, ErrorMessageOnFailure = "Custom error" };
        var cancellationToken = CancellationToken.None;

        var result = GoogleSheets.ReadSheet(input, connection, options, cancellationToken);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
        Assert.That(result.Error.Message, Contains.Substring("Custom error"));
    }
}
