namespace Startkicker.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
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

        public Image GetById(int id)
        {
            Image result = this.imagesRepo.GetById(id);
            if (result != null && !result.IsRemoved)
            {
                return result;
            }

            return null;
        }

        public Image GetByUrl(string url)
        {
            Image result = this.imagesRepo.All().First(i => i.ImageUrl == url);
            if (result != null && !result.IsRemoved)
            {
                return result;
            }

            return null;
        }

        public void Add(Image image)
        {
            this.imagesRepo.Add(image);
            this.imagesRepo.SaveChanges();
        }

        public void Update(Image image)
        {
            this.imagesRepo.Update(image);
            this.imagesRepo.SaveChanges();
        }

        public void Remove(Image image)
        {
            image.IsRemoved = true;
            this.imagesRepo.Update(image);
            this.imagesRepo.SaveChanges();

        }
        public async Task<string> UploadAsync(byte[] content, string extension)
        {
            string guid = Guid.NewGuid().ToString();
            string imageUrl = string.Format("/{0}.{1}", guid, extension);

            var dbx = new DropboxClient(token);

            using (var mem = new MemoryStream(content))
            {
                var image = await dbx.Files.UploadAsync(new CommitInfo(imageUrl), body: mem);
                var shareLink = await dbx.Sharing.CreateSharedLinkAsync(image.PathLower);
                var rawLink = ProcessImageLink(shareLink.Url);

                return rawLink;
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
