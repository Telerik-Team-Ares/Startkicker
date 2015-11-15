namespace Startkicker.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Startkicker.Data.Models;
    using Startkicker.Data.Repositories;
    using Startkicker.Services.Data.Contracts;

    using Dropbox.Api;
    using Dropbox.Api.Files;

    public class ImagesService : IImagesService
    {
        private const string token = "LMSUBAH0x_AAAAAAAAAAHYdRzBEy_KFWb3wKOGHID8Wih_H3izmylXokSUUhj824";
        private readonly IRepository<Image> imagesRepo;

        public ImagesService(IRepository<Image> imagesRepo)
        {
            this.imagesRepo = imagesRepo;
        }

        public async Task<string> UploadAsync(Stream stream)
        {
            string guid = Guid.NewGuid().ToString();
            string extension = "jpg";

            string imageUrl = string.Format("/{0}.{1}", guid, extension);

            using (var dbx = new DropboxClient(token))
            {
                var image = await dbx.Files.UploadAsync(new CommitInfo(imageUrl), stream);
                var shareLink = await dbx.Sharing.CreateSharedLinkAsync(image.PathLower);
                return ProcessImageLink(shareLink.Url);
            }
        }

        private string ProcessImageLink(string link)
        {
            string rawExtension = "raw=1"; // Magic, don't touch

            string cutLink = link.Remove(link.Length - 4);
            string processedLink = cutLink + rawExtension;

            return processedLink;
        }
    }
}
