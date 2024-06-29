using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IImageService
    {
        string UploadImage(IFormFile image);
        void DeleteImage(string imagePath);
        IEnumerable<string> UploadImages(IEnumerable<IFormFile> images);
    }
}
