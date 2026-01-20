using System.Threading.Tasks;

namespace cctvtemplate.Helpers
{
    public static class FileHelper
    {
        public static bool CheckSize(this IFormFile file , int mb)
        {
            return file.Length < mb * 1024 * 1024;
        }

        public static bool CheckType(this IFormFile file , string type)
        {
            return file.ContentType.Contains(type);
        }

        public static async Task<string> FileUpload(this IFormFile file , string folderPath)
        {
            string uniqueImgPath = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath, uniqueImgPath);

            using FileStream stream = new(path, FileMode.Create);

            await file.CopyToAsync(stream);

            return uniqueImgPath;
        }







        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        } 
    }
}
