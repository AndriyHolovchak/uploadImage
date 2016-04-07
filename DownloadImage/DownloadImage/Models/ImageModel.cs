using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownloadImage.Models
{
    public class ImageModel
    {
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public string StatusCode { get; set; }
    }
}