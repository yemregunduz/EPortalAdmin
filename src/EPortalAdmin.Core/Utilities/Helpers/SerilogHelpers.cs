using EPortalAdmin.Core.Logging;
using Serilog.Context;
using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace EPortalAdmin.Core.Utilities.Helpers
{
    public static class SerilogHelpers
    {
        public static ColumnOptions GetLogTableColumnOptions() => GetColumnOptions(GetAdditionalDataColumnsForLogTable());

        public static ColumnOptions GetExceptionLogTableColumnOptions() => GetColumnOptions(GetAdditionalDataColumnsForExceptionLogTable());

        public static void PushServiceLogProperty(ServiceLog serviceLog)
        {
            LogContext.PushProperty("Action", serviceLog.Action);
            LogContext.PushProperty("Controller", serviceLog.Controller);
            LogContext.PushProperty("IpAddress", serviceLog.IpAddress);
            LogContext.PushProperty("HttpMethod", serviceLog.HttpMethod);
            LogContext.PushProperty("ResponseHttpStatusCode", serviceLog.ResponseHttpStatusCode);
            LogContext.PushProperty("QueryString", serviceLog.QueryString);
            LogContext.PushProperty("BrowserName", serviceLog.BrowserName);
            LogContext.PushProperty("HttpHeaders", serviceLog.HttpHeaders);
            LogContext.PushProperty("RouteValuesJson", serviceLog.RouteValuesJson);
            LogContext.PushProperty("UserId", serviceLog.UserId);
            LogContext.PushProperty("RequestBody", serviceLog.RequestBody);
            LogContext.PushProperty("ResponseBody", serviceLog.ResponseBody);
            LogContext.PushProperty("ResponseTimeInMilliseconds", serviceLog.ResponseTimeInMilliseconds);
            LogContext.PushProperty("CorrelationId", serviceLog.CorrelationId);
            LogContext.PushProperty("EndpointId", serviceLog.EndpointId);
        }

        public static void PushExceptionLogProperty(ExceptionLog exceptionLog)
        {
            LogContext.PushProperty("CorrelationId", exceptionLog.CorrelationId);
            LogContext.PushProperty("Title", exceptionLog.Title);
            LogContext.PushProperty("ExceptionMessage", exceptionLog.ExceptionMessage);
            LogContext.PushProperty("InnerExceptionMessage", exceptionLog.InnerExceptionMessage);
            LogContext.PushProperty("StackTrace", exceptionLog.StackTrace);
            LogContext.PushProperty("ValidationErrors", exceptionLog.ValidationErrors);
            LogContext.PushProperty("Type", exceptionLog.Type);
            LogContext.PushProperty("HttpStatusCode", exceptionLog.HttpStatusCode);
            LogContext.PushProperty("Instance", exceptionLog.Instance);
        }

        private static void ConfigureColumnOptions(ColumnOptions columnOptions)
        {
            columnOptions.Store.Remove(StandardColumn.Message);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.Exception);
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
        }

        private static void SetAdditionalDataColumns(ColumnOptions columnOptions, IEnumerable<DataColumn> additionalDataColumns)
        {
            columnOptions.AdditionalDataColumns = new List<DataColumn>(additionalDataColumns);
        }

        private static IEnumerable<DataColumn> GetAdditionalDataColumnsForLogTable()
        {
            yield return new() { DataType = typeof(int), ColumnName = "UserId" };
            yield return new() { DataType = typeof(Guid), ColumnName = "CorrelationId" };
            yield return new() { DataType=typeof(int), ColumnName= "EndpointId" };
            yield return new() { DataType = typeof(string), ColumnName = "Controller" };
            yield return new() { DataType = typeof(string), ColumnName = "Action" };
            yield return new() { DataType = typeof(string), ColumnName = "QueryString" };
            yield return new() { DataType = typeof(string), ColumnName = "HttpMethod" };
            yield return new() { DataType = typeof(long), ColumnName = "ResponseTimeInMilliseconds" };
            yield return new() { DataType = typeof(string), ColumnName = "IpAddress" };
            yield return new() { DataType = typeof(string), ColumnName = "BrowserName" };
            yield return new() { DataType = typeof(int), ColumnName = "ResponseHttpStatusCode" };
            yield return new() { DataType = typeof(string), ColumnName = "RequestBody" };
            yield return new() { DataType = typeof(string), ColumnName = "ResponseBody" };
            yield return new() { DataType = typeof(string), ColumnName = "HttpHeaders" };
            yield return new() { DataType = typeof(string), ColumnName = "RouteValuesJson" };
        }

        private static IEnumerable<DataColumn> GetAdditionalDataColumnsForExceptionLogTable()
        {
            yield return new() { DataType = typeof(Guid), ColumnName = "CorrelationId" };
            yield return new() { DataType = typeof(string), ColumnName = "Type" };
            yield return new() { DataType = typeof(int), ColumnName = "HttpStatusCode" };
            yield return new DataColumn { DataType = typeof(string), ColumnName = "Title" };
            yield return new DataColumn { DataType = typeof(string), ColumnName = "ExceptionMessage" };
            yield return new DataColumn { DataType = typeof(string), ColumnName = "InnerException" };
            yield return new DataColumn { DataType = typeof(string), ColumnName = "StackTrace" };
        }

        private static ColumnOptions GetColumnOptions(IEnumerable<DataColumn> additionalDataColumns)
        {
            var columnOptions = new ColumnOptions();
            ConfigureColumnOptions(columnOptions);
            SetAdditionalDataColumns(columnOptions, additionalDataColumns);
            return columnOptions;
        }

    }
}
