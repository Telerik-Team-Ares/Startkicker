namespace Startkicker.Api.Common
{
    public interface IEncrypter
    {
        string Encrypt(string plainText);

        string Decrypt(string cipherText);
    }
}
