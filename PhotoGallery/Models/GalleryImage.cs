using System.Windows.Media.Imaging;

namespace PhotoGallery.Models;

public class GalleryImage
{
    public BitmapImage Image { get; set; }

    public string Description { get; set; }
}