namespace Startkicker.Services.Data.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IImagesService
    {
        Task<string> UploadAsync(Stream stream);
    }
}
