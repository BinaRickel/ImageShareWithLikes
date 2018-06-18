using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication21.Data;
using WebApplication21.Models;

namespace WebApplication21.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new ImageRepository(Properties.Settings.Default.ConStr);
            return View(repo.GetImages());
        }
        [HttpPost]
        public ActionResult Upload(Image image, HttpPostedFileBase imageFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            imageFile.SaveAs(Server.MapPath("~/UploadedImages/") + fileName);
            image.FileName = fileName;
            var repo = new ImageRepository(Properties.Settings.Default.ConStr);
            repo.Add(image);
            return Redirect("/home/index");
        }
        public ActionResult UploadImages()
        {
            return View();
        }
        public ActionResult ViewImage(int id)
        {
            var repo = new ImageRepository(Properties.Settings.Default.ConStr);
            var VIM = new ViewImageModel { Image = repo.GetImagebyId(id) };
            if (HasPermission(id))
            {
                VIM.HasPermission = true;
            }
            else
            {
                VIM.HasPermission = false;
            }
            return View(VIM);
        }
        private bool HasPermission(int id)
        {
            if (Session["allowedids"] == null)
            {
                return false;
            }

            var allowedIds = (List<int>)Session["allowedids"];
            return allowedIds.Contains(id);
        }
        [HttpPost]
        public void Like(int id)
        {
            var repo = new ImageRepository(Properties.Settings.Default.ConStr);
            repo.AddLike(id);
            List<int> allowedIds;
            if (Session["allowedids"] == null)
            {
                allowedIds = new List<int>();
                Session["allowedids"] = allowedIds;
            }
            else
            {
                allowedIds = (List<int>)Session["allowedids"];
            }

            allowedIds.Add(id);
        }
        public ActionResult GetLikes(int id)
        {
            var repo = new ImageRepository(Properties.Settings.Default.ConStr);
            return Json(repo.GetImagebyId(id).LikeCount, JsonRequestBehavior.AllowGet);
        }
    }
}