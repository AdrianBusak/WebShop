using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.ImageRepo
{
    public interface IImageRepository
    {
        List<Image> GetAllByProductId(int productId);
        Image? GetById(int id);
        void Add(Image image);
        void Update(Image image);
        void Delete(Image image);
        void DeleteRange(ICollection<Image> images);
    }
}
