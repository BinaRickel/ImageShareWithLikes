using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication21.Data;

namespace WebApplication21.Models
{
    public class ViewImageModel
    {
        public Image Image { get; set; }
        public bool HasPermission { get; set; }
    }
}