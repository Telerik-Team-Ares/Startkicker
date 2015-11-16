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
            : this(new StringCipherEncrypter())
        {

        }

        public DecryptInputIdAttribute(IEncrypter encrypter)
        {
            this.encrypter = encrypter;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var arguments = actionContext.ActionArguments;

            foreach (var argument in arguments)
            {
                if (!argument.Value.GetType().IsValueType && (argument.Value.GetType().Name != typeof(string).Name))
                {
                    var objectContent = argument.Value;

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
                if (argument.Key == "id" || argument.Key.EndsWith("Id"))
                {
                    var key = argument.Key;
                    arguments[key] = this.encrypter.Decrypt(arguments[key].ToString());
                }
            }
        }
    }
}