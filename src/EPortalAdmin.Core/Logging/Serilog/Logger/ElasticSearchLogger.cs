using EPortalAdmin.Core.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace EPortalAdmin.Core.Logging.Serilog.Logger
{
    public class ElasticSearchLogger : LoggerServiceBase
    {
        public ElasticSearchLogger(IConfiguration configuration)
        {
            ElasticSearchOptions options = configuration.GetSection(ElasticSearchOptions.AppSettingsKey).Get<ElasticSearchOptions>()
                            ?? throw new NullReferenceException(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                .WriteTo
                .Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(options.ConnectionString))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                    }
                ).CreateLogger();
        }
    }
}
