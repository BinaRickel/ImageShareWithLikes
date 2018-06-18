using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication21.Data
{
    public class ImageRepository
    {
        private string _connectionString;

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Image> GetImages()
        {
            using (var context = new imageuploadDataContext(_connectionString))
            {
                return context.Images.OrderByDescending(i => i.Id).ToList();
            }
        }
        public void Add(Image image)
        {
            using (var context = new imageuploadDataContext(_connectionString))
            {
                context.Images.InsertOnSubmit(image);
                context.SubmitChanges();
            }
        }
        public Image GetImagebyId(int id)
        {
            using (var context = new imageuploadDataContext(_connectionString))
            {
                return context.Images.FirstOrDefault(p => p.Id == id);
            }
        }
        public void AddLike(int id)
        {
            using (var context = new imageuploadDataContext(_connectionString))
            {
                var image = context.Images.FirstOrDefault(i => i.Id == id);
                image.LikeCount++;
                context.SubmitChanges();
            }
        }
        
    }
}
