using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPhotoStock.Models
{
    public static class SessionManager
    {
        public static UserInfo CurrentUser { get; set; }
        public static string Token { get; set; }
    }
}
