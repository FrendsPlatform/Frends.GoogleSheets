using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Frends.GoogleSheets.ReadSheet.Definitions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json.Linq;

namespace Frends.GoogleSheets.ReadSheet;

/// <summary>
/// Task class.
/// </summary>
public static class GoogleSheets
{
    /// <summary>
    /// Reads data from a specified range in a Google Sheets spreadsheet.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-GoogleSheets-ReadSheet)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <param name="connection">Connection parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, dynamic (JArray) Data, string Range, string MajorDimension, string ETag, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static Result ReadSheet(
        [PropertyTab] Input input,
        [PropertyTab] Connection connection,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            var credential = GoogleCredential.FromJson(connection.ServiceAccountJson)
                .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);

            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Frends.GoogleSheets.ReadSheet",
            });

            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(input.SpreadsheetId, input.Range);

            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;

            return new Result
            {
                Success = true,
                Data = ListToJToken(values, cancellationToken),
                ETag = response.ETag,
                Range = response.Range,
                MajorDimension = response.MajorDimension,
                Error = null,
            };
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            if (options.ThrowErrorOnFailure)
            {
                if (string.IsNullOrEmpty(options.ErrorMessageOnFailure))
                    throw new Exception(e.Message, e);

                throw new Exception(options.ErrorMessageOnFailure, e);
            }

            var errorMessage = !string.IsNullOrEmpty(options.ErrorMessageOnFailure)
                ? $"{options.ErrorMessageOnFailure}: {e.Message}"
                : e.Message;

            return new Result
            {
                Success = false,
                Data = null,
                Error = new Error
                {
                    Message = errorMessage,
                    AdditionalInfo = e,
                },
            };
        }
    }

    private static JArray ListToJToken(IList<IList<object>> list, CancellationToken cancellationToken)
    {
        var array = new JArray();
        foreach (var row in list)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var rowArray = new JArray();
            foreach (var cell in row)
            {
                cancellationToken.ThrowIfCancellationRequested();
                rowArray.Add(JToken.FromObject(cell));
            }

            array.Add(rowArray);
        }

        return array;
    }
}