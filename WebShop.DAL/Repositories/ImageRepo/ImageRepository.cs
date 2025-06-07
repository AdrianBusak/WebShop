using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.ImageRepo
{
    public class ImageRepository : IImageRepository
    {
        private readonly WebShopContext _context;

        public ImageRepository(WebShopContext context)
        {
            _context = context;
        }

        public List<Image> GetAllByProductId(int productId) =>
            _context.Images.Where(i => i.ProductId == productId).ToList();

        public Image? GetById(int id) =>
            _context.Images.FirstOrDefault(i => i.Id == id);

        public void Add(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public void Update(Image image)
        {
            _context.SaveChanges();
        }

        public void Delete(Image image)
        {
            _context.Images.Remove(image);
            _context.SaveChanges();
        }

        public void DeleteRange(ICollection<Image> images)
        {
            if (images != null && images.Count > 0)
            {
                _context.Images.RemoveRange(images);
                _context.SaveChanges();
            }
        }

    }
}
