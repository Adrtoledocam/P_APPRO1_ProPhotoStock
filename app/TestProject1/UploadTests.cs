using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class UploadTests
    {
        [Fact]
        public void Upload_EmptyTitleOrFile()
        {
            string title = "";
            byte[] fileData = null;
            bool canSubmit;

            if (string.IsNullOrEmpty(title) || fileData == null)
                canSubmit = false;
            else
                canSubmit = true;

            Assert.False(canSubmit);
        }

        [Fact]
        public void Upload_ValidData()
        {
            string title = "Sunset in Lausanne";


            byte[] fileData = new byte[] { 0x20, 0x21, 0x22 };
            bool canSubmit;
            // Fake image
            if (string.IsNullOrEmpty(title) || fileData == null)
                canSubmit = false;
            else
                canSubmit = true;

            Assert.True(canSubmit);
        }
    }
}
