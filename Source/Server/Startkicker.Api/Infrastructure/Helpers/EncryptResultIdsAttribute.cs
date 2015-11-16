namespace Startkicker.Api.Infrastructure.Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using Startkicker.Api.Common;

    public class EncryptResultIdsAttribute : ActionFilterAttribute
    {
        private readonly IEncrypter encrypter;

        public EncryptResultIdsAttribute()
            : this(new StringCipherEncrypter())
        {

        }

        public EncryptResultIdsAttribute(IEncrypter encrypter)
        {
            this.encrypter = encrypter;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                var type = objectContent.ObjectType;

                var value = objectContent.Value;

                if (string.Equals(type.Name, (typeof(ICollection<>)).Name))
                {
                    foreach (var val in (value as ICollection))
                    {
                        var props = val.GetType().GetProperties().Where(x => x.Name.EndsWith("Id"));

                        foreach (var propertyInfo in props)
                        {
                            propertyInfo.SetValue(val, this.encrypter.Encrypt(propertyInfo.GetValue(val, null).ToString()));
                        }
                    }
                }
                else
                if ((!type.IsValueType) && (type.Name != typeof(string).Name))
                {
                    var props = value.GetType().GetProperties().Where(x => x.Name.EndsWith("id") || x.Name.EndsWith("Id"));

                    foreach (var propertyInfo in props)
                    {
                        propertyInfo.SetValue(value, this.encrypter.Encrypt(propertyInfo.GetValue(value, null).ToString()));
                    }
                }
            }
        }
    }
}