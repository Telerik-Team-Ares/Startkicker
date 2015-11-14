namespace Startkicker.Api
{
    using System.Web.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonResultConfig
    {
        public static void CemalCaseConfig()
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}