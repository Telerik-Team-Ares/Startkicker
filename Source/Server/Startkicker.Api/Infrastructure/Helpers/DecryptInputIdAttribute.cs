namespace Startkicker.Api.Infrastructure.Helpers
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Startkicker.Api.Common;

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

            var keys = arguments.Keys.Where(x => x == "id" || x.EndsWith("Id")).ToList();

            foreach (var key in keys)
            {
                arguments[key] = this.encrypter.Decrypt(arguments[key].ToString());
            }
        }
    }
}