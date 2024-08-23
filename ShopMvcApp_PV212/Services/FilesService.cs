﻿using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Services
{
    public class FilesService : IFilesService
    {
        const string imageFolder = "images";
        private readonly IWebHostEnvironment environment;

        public FilesService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public Task DeleteProductImage(string path)
        {
            string root = environment.WebRootPath;
            string fullPath = root + path;

            if (File.Exists(fullPath))            
                return Task.Run(() => File.Delete(fullPath));
                
            return Task.CompletedTask;
        }

        public async Task<string> EditProductImage(string oldPath, IFormFile newFile)
        {
            await DeleteProductImage(oldPath);
            return await SaveProductImage(newFile);
        }

        public async Task<string> SaveProductImage(IFormFile file)
        {
            // get image destination path
            string root = environment.WebRootPath;      // wwwroot
            string name = Guid.NewGuid().ToString();    // random name
            string extension = Path.GetExtension(file.FileName); // get original extension
            string fullName = name + extension;         // full name: name.ext

            // create destination image file path
            string imagePath = Path.Combine(imageFolder, fullName);
            string imageFullPath = Path.Combine(root, imagePath);

            // save image on the folder
            using (FileStream fs = new FileStream(imageFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            // return image file path
            return Path.DirectorySeparatorChar + imagePath;
        }
    }
}
