using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Core.Logic
{
    public class FilesManager
    {
        public bool IsExtenstions(List<string> Extenstions, IFormFile file)
        {
            if (Extenstions.Select(s => s.ToLower()).Contains(Path.GetExtension(file.FileName.ToLower())))
                return true;
            else
                return false;
        }
        public bool IsExtenstion(string Extenstions, IFormFile file)
        {
            if (Extenstions.ToLower().Contains(Path.GetExtension(file.FileName.ToLower())))
                return true;
            else
                return false;
        }

        public bool IsSizeSucceeded(long SizeByte, IFormFile file)
        {
            if (file.Length <= SizeByte)
                return true;
            else
                return false;
        }

        public byte[] UploadInDb(IFormFile file)
        {
            using(var MemoryStream = new MemoryStream())
            {
                file.CopyTo(MemoryStream);
                return MemoryStream.ToArray();
            }
        }
    }
}
