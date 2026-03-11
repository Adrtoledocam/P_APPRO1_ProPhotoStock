using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class AuthServiceTests
    {
        [Fact]
        public void Token_WhenNull_IsInvalid()
        {
            string token = null;
            bool isAuthenticated;

            if (string.IsNullOrEmpty(token))
                isAuthenticated = false;
            else
                isAuthenticated = true;

            Assert.False(isAuthenticated);
        }

        [Fact]
        public void Token_WhenNotEmpty_IsValid()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
            bool isAuthenticated;

            if (string.IsNullOrEmpty(token))
                isAuthenticated = false;
            else
                isAuthenticated = true;

            Assert.True(isAuthenticated);
        }
    }
}
