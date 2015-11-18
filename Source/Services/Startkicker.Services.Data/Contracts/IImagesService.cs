namespace Startkicker.Services.Data.Contracts
{
    using System.IO;
    using System.Threading.Tasks;

    using Startkicker.Data.Models;

    public interface IImagesService
    {
        Image GetById(int id);

        Image GetByUrl(string url);

        void Add(Image image);

        void Update(Image image);

        void Remove(Image image);

        Task<string> UploadAsync(Stream stream);
    }
}
