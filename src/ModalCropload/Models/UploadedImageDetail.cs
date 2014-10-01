namespace ModalCropload.Models
{
    public class UploadedImageDetail
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int PreviewImageWidth { get; set; }

        public int PreviewImageHeight { get; set; }

        public string ImgKey { get; set; }
    }
}