using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Filters
{
    public class ImageExtensionValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return true;
            }

            try
            {
                using (var image = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(image.RawFormat) ||
                        ImageFormat.Png.Equals(image.RawFormat) ? true : false;
                }
            }
            catch (ArgumentException e) { }

            return false;
        }
    }
}