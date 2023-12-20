using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EPortalAdmin.Core.FileStorage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public LocalStorage(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task DeleteAsync(string path, string fileName)
            => System.IO.File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, path, fileName));

        public IList<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public new bool HasFile(string path, string fileName)
        {
            var res = System.IO.File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, path, fileName));
            return res;
        }

        public async Task<IList<(string fileName, string pathOrContainerName)>> BulkUploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                var data = await UploadAsync(path, file);
                datas.Add(data);
            }

            return datas;
        }

        public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string path, IFormFile file)
        {
            string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string fileNewName = await FileRenameAsync(path, file.FileName, HasFile);

            await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);

            return (fileNewName, path);
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
