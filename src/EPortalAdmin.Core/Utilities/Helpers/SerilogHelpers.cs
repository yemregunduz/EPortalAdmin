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

        public static void PushLogDetailProperty(LogDetail logDetail)
        {
            LogContext.PushProperty("Action", logDetail.Action);
            LogContext.PushProperty("Controller", logDetail.Controller);
            LogContext.PushProperty("IpAddress", logDetail.IpAddress);
            LogContext.PushProperty("HttpMethod", logDetail.HttpMethod);
            LogContext.PushProperty("ResponseHttpStatusCode", logDetail.ResponseHttpStatusCode);
            LogContext.PushProperty("QueryString", logDetail.QueryString);
            LogContext.PushProperty("BrowserName", logDetail.BrowserName);
            LogContext.PushProperty("HttpHeaders", logDetail.HttpHeaders);
            LogContext.PushProperty("RouteValuesJson", logDetail.RouteValuesJson);
            LogContext.PushProperty("UserId", logDetail.UserId);
            LogContext.PushProperty("RequestBody", logDetail.RequestBody);
            LogContext.PushProperty("ResponseBody", logDetail.ResponseBody);
            LogContext.PushProperty("ResponseTimeInMilliseconds", logDetail.ResponseTimeInMilliseconds);
            LogContext.PushProperty("CorrelationId", logDetail.CorrelationId);
            LogContext.PushProperty("EndpointId", logDetail.EndpointId);
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
