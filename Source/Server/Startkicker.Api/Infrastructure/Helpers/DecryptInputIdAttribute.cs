namespace Startkicker.Api.Infrastructure.Helpers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Startkicker.Api.Common;
    using Startkicker.Api.Common.Contracts;

    public class DecryptInputIdAttribute : ActionFilterAttribute
    {
        private readonly IEncrypter encrypter;

        public DecryptInputIdAttribute()
            : this(new UrlIdEncoder())
        {

        }

        public DecryptInputIdAttribute(IEncrypter encrypter)
        {
            this.encrypter = encrypter;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var arguments = actionContext.ActionArguments;

            for (var i =0; i<arguments.Keys.Count;i++)//var argkKey in arguments.Keys)
            {
                var argkKey = arguments.Keys.ElementAt(i);
                var argument = arguments[argkKey];
                if (!argument.GetType().IsValueType && (argument.GetType().Name != typeof(string).Name))
                {
                    var objectContent = argument;

                    var props =
                        objectContent.GetType().GetProperties().Where(x => x.Name.EndsWith("id") || x.Name.EndsWith("Id"));

                    foreach (var propertyInfo in props)
                    {
                        propertyInfo.SetValue(
                            objectContent,
                            this.encrypter.Decrypt(propertyInfo.GetValue(objectContent, null).ToString()));
                    }
                }
                else
                if (argkKey == "id" || argkKey.EndsWith("Id"))
                {
                   // var key = argument.Key;
                    arguments[argkKey] = this.encrypter.Decrypt(arguments[argkKey].ToString());
                }
            }
        }
    }
}