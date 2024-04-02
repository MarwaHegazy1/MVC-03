﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ASP.NET_Core_MVC_03.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file ,string folderName)
        {
            string folderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files" ,folderName);
            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
        public static void DeleteFile(string fileName,string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName); 
            if(File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
