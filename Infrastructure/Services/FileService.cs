using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<bool> SaveImage(IFormFile imageFile)
        {

            if (imageFile != null && imageFile.Length > 0)
            {
                var filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Resources", "Uploads"));
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using (var fileStream = new FileStream(Path.Combine(filePath, imageFile.FileName), FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }
            return true;
        }


    }
}
