namespace Startkicker.Api.Common
{
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using Startkicker.Api.Common.Contracts;

    public class UrlIdEncoder : IEncrypter
    {
        private readonly Base64UrlTextEncoder encoder;

        public UrlIdEncoder()
        {
            this.encoder = new Base64UrlTextEncoder();
        }

        public string Encrypt(string plainText)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return this.encoder.Encode(bytes);
        }

        public string Decrypt(string cipherText)
        {
            byte[] bytes = this.encoder.Decode(cipherText);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}