using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleSheets.CreateSheet.Definitions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Frends.GoogleSheets.CreateSheet;

/// <summary>
/// Task class.
/// </summary>
public static class GoogleSheets
{
    /// <summary>
    /// Creates a new sheet in a specified Google Sheets spreadsheet.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-GoogleSheets-CreateSheet)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <param name="connection">Connection parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, int NewSheetId, string ETag, object Error { string Message, Exception AdditionalInfo } }</returns>
    public static async Task<Result> CreateSheet(
        [PropertyTab] Input input,
        [PropertyTab] Connection connection,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            var credential = GoogleCredential.FromJson(connection.ServiceAccountJson)
                .CreateScoped(SheetsService.Scope.Spreadsheets);

            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Frends.GoogleSheets.CreateSheet",
            });

            var addSheetRequest = new AddSheetRequest
            {
                Properties = new SheetProperties
                {
                    Title = input.NewSheetName,
                    Hidden = input.Hidden,
                },
            };

            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
            {
                Requests = [new Request { AddSheet = addSheetRequest }],
            };

            var request = service.Spreadsheets.BatchUpdate(batchUpdateRequest, input.SpreadsheetId);
            var response = await request.ExecuteAsync(cancellationToken);

            var sheet = response.Replies[0].AddSheet.Properties;
            return new Result
            {
                Success = true,
                NewSheetId = sheet.SheetId ?? 0,
                ETag = response.ETag,
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
                Error = new Error
                {
                    Message = errorMessage,
                    AdditionalInfo = e,
                },
            };
        }
    }
}