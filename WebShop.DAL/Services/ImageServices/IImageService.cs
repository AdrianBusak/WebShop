using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.ImageServices
{
    public interface IImageService
    {
        List<Image> GetAllByProductId(int productId);
        Image? GetById(int id);
        void Create(Image image);
        void Update(Image image);
        void Delete(Image image);
    }
}
