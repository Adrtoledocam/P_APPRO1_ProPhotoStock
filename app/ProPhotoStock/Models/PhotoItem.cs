using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPhotoStock.Models
{
    public class PhotoItem
    { 
        public int photoId { get; set; }
        public string photoTitle { get; set; }
        public string photoUrl { get; set; }
        public string useName { get; set; }
        public string tagName { get; set; } 
    }
}
