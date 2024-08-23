using Core.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICartService
    {
        int GetCount();
        void AddItem(int id);
        void RemoveItem(int id);
        List<ProductDto> GetProducts();
        List<Product> GetProductsEntity();
        void Clear();
    }
}
