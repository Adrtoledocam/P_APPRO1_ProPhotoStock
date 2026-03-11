using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class UserRoleTests
    {
        [Fact]
        public void UserRole_Client_CannotUpload()
        {
            string userRole = "client";
            bool canUpload;

            if (userRole == "photographe")
                canUpload = true;
            else
                canUpload = false;

            Assert.False(canUpload);
        }

        [Fact]
        public void UserRole_Photographe_CanUpload()
        {
            string userRole = "photographe";
            bool canUpload;

            if (userRole == "photographe")
                canUpload = true;
            else
                canUpload = false;

            Assert.True(canUpload);
        }
    }
}
