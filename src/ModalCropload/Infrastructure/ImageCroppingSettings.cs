using System;

namespace ModalCropload.Infrastructure
{
    public class ImageCroppingSettings
    {
        public string OriginalImagePath { get; private set; }
        public string UniqueKey { get; set; }
        public string ImageNamePrefix { get; set; }
        public string OutputExtension { get; set; }
        public string OutputFolderPath { get; set; }
        
        public int ScaleWidth { get; set; }
        public string CropParameters { get; set; }

        //$-→ Constructor
        public ImageCroppingSettings(string originalImagePath) {
            OriginalImagePath = originalImagePath;
            UniqueKey = new Guid().ToString("D");
            ImageNamePrefix = "Photo";
            OutputExtension = ".jpg";
            OutputFolderPath = "~/Uploads/Images/Newscast";
            ScaleWidth = 750;
        }
    }
}