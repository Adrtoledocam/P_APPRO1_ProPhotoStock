using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPhotoStock.Models
{
    class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsVisible { get; set; }
        public string Status { get; set; }
    }
}
