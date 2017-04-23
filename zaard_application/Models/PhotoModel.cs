using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;

namespace zaard_application.Models
{
    public class PhotoModel
    {
        public int ID { get; set; }
        public string ImageFile { get; set; } //This contains a path where the image is locally stored on the server
        public string Caption { get; set; }
        byte[] Image { get; set; }
    }
}
