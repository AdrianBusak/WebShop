using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ImageRepo;

namespace WebShop.DAL.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;

        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public List<Image> GetAllByProductId(int productId) =>
            _repository.GetAllByProductId(productId);

        public Image? GetById(int id) =>
            _repository.GetById(id);

        public void Create(Image image) =>
            _repository.Add(image);

        public void Update(Image image) =>
            _repository.Update(image);

        public void Delete(Image image) =>
            _repository.Delete(image);

        public void AddRange(IEnumerable<Image> images)
        {
            if (images == null || !images.Any())
            {
                throw new ArgumentException("The collection of images cannot be null or empty.", nameof(images));
            }
            images.ToList()
                .ForEach(image => _repository
                .Add(image));
        }
    }
}
