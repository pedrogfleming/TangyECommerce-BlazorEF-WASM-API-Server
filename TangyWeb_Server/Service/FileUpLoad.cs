using Microsoft.AspNetCore.Components.Forms;
using TangyWeb_Server.Service.IService;

namespace TangyWeb_Server.Service
{
    public class FileUpLoad : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpLoad(IWebHostEnvironment webHostEnvironment = null)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string filePath)
        {
            if (File.Exists(_webHostEnvironment.WebRootPath + filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath + filePath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images\\product";
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            var filePath = Path.Combine(folderDirectory, fileName);
            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            //We copy the file we recieved to the created in the location
            await file.OpenReadStream().CopyToAsync(fs);
            var fullpath = $"/images/product/{fileName}";
            return fullpath;
        }
    }
}
