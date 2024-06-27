using Microsoft.AspNetCore.Http;
using StayStop.BLL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace StayStop.BLL_EF.Service
{
    public class ImageService : IImageService
    {
        private readonly List<string> correctExtentions = [".jpg", ".png", ".jfif"];
        private readonly string rootPath = Directory.GetCurrentDirectory();

        public void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(rootPath, Path.Combine("Images", imagePath));
            File.Delete(fullPath);
        }

        public string UploadImage(IFormFile image)
        {
            string extention = Path.GetExtension(image.FileName);
            if (!correctExtentions.Contains(extention))
            {
                throw new InvalidDataException($"Provide file with extention: ({string.Join(',', correctExtentions)})");
            }

            var fullPath = Path.Combine(rootPath, Path.Combine("Images", image.FileName));
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return fullPath;
        }
    }
}
