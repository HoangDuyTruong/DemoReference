using System;
using System.IO;

namespace VideosConverter.Helper
{
    public static class SupportVideo
    {
        public static string PreventDuplicateFile(string fullName,string pathToSave,int index)
        {
            var fileName = fullName;
            if (index != 0)
            {
                var ext = Path.GetExtension(fullName);
                var name = Path.GetFileName(fullName);
                if (name != null && ext != null) 
                    fileName = $"{name}({index}){ext}";
                else
                    fileName = $"{fullName}({index})";
                
            }
            var fullPath = Path.Combine(pathToSave, fileName);
            if (File.Exists(fullPath))
            {      
                fileName=  PreventDuplicateFile(fullName, pathToSave, index + 1);
            }
            return fileName;
        }
        
        public static string RandomNameFile(string extension)
        {
            var fileNameDefault = Path.GetRandomFileName();
            if (extension == null)
                return fileNameDefault;
            var nameRandom = Path.GetFileName(fileNameDefault);
            return  $"{nameRandom}{extension}";
        }

        public static string CreatePathFolder()
        {
            var dateUpload = DateTime.Now.ToString("yyyyMMdd");
            var folderRoot = Path.Combine("Root", "FileUpload");
            var folderUpload = Path.Combine(folderRoot, dateUpload);
            return folderUpload;
        }
        
        public static string CreatePathToSave(string folderUpload)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string requiredPath = Directory.GetParent(Directory.GetParent(workingDirectory).Parent.FullName).ToString();
            var pathToSave = Path.Combine(requiredPath, folderUpload);
            if (!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);
            return pathToSave;
        }

    }
}