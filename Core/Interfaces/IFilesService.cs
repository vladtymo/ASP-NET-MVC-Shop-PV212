using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFilesService
    {
        Task<string> SaveProductImage(IFormFile file);
        Task DeleteProductImage(string path);
        Task<string> EditProductImage(string oldPath, IFormFile newFile);
    }
}
