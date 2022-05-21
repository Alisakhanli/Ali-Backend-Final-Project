using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HotelProject.Areas.Admin.Utils
{
    public class FileUtils
    {
        public static async Task<bool> DelteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        public static string CreateFile(string folderPath, IFormFile file)
        {
            string fileName = file.FileName;
            var path = Path.Combine(folderPath, fileName);
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return fileName;
        }
    }
}
