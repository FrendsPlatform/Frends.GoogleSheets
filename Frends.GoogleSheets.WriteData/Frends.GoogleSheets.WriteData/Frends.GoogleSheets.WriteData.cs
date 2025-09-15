using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleSheets.WriteData.Definitions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Frends.GoogleSheets.WriteData;

/// <summary>
/// Task class.
/// </summary>
public static class GoogleSheets
{
    /// <summary>
    /// Reads data from a specified range in a Google Sheets spWriteData.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends-GoogleSheets-WriteData)
    /// </summary>
    /// <param name="input">Essential parameters.</param>
    /// <param name="connection">Connection parameters.</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">A cancellation token provided by Frends Platform.</param>
    /// <returns>object { bool Success, dynamic (JArray) Data, string Range, string MajorDimension, string ETag, object Error { string Message, dynamic AdditionalInfo } }</returns>
    public static async Task<Result> WriteData(
        [PropertyTab] Input input,
        [PropertyTab] Connection connection,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            var credential = GoogleCredential
                .FromJson(connection.ServiceAccountJson)
                .CreateScoped("https://www.googleapis.com/auth/spreadsheets");

            var service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Frends",
            });

            // Deserialize the JSON string into 2D object array
            var values = JsonConvert.DeserializeObject<IList<IList<object>>>(input.Values);

            var valueRange = new ValueRange
            {
                Values = values,
            };

            UpdateValuesResponse updateValuesResponse;
            if (options.InsertDataMode == InsertDataModes.UpdateOverwrite)
            {
                var updateRequest = service.Spreadsheets.Values.Update(valueRange, input.SpreadsheetId, input.StartingCell);
                updateRequest.ValueInputOption = options.ValueInputMode.ToGoogleApiUpdateValue();
                updateValuesResponse = await updateRequest.ExecuteAsync(cancellationToken);
            }
            else
            {
                var appendRequest = service.Spreadsheets.Values.Append(valueRange, input.SpreadsheetId, input.StartingCell);
                appendRequest.ValueInputOption = options.ValueInputMode.ToGoogleApiAppendValue();
                appendRequest.InsertDataOption = options.InsertDataMode.ToGoogleApiAppendValue();
                var response = await appendRequest.ExecuteAsync(cancellationToken);
                updateValuesResponse = response.Updates;
            }

            return new Result
            {
                Success = true,
                UpdatedRange = updateValuesResponse.UpdatedRange,
                UpdatedRows = updateValuesResponse.UpdatedRows ?? 0,
                UpdatedColumns = updateValuesResponse.UpdatedColumns ?? 0,
                ETag = updateValuesResponse.ETag,
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
                    AdditionalInfo = new
                    {
                        Exception = e,
                    },
                },
            };
        }
    }
}