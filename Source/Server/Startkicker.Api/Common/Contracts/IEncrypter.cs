namespace Startkicker.Api.Common.Contracts
{
    public interface IEncrypter
    {
        string Encrypt(string plainText);

        string Decrypt(string cipherText);
    }
}
