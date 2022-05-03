using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using PhotoGallery.DataTypes;

namespace PhotoGallery.Models;

public class ProgramFile
{
    private ObservableLinkedList<GalleryImage> _images = new ObservableLinkedList<GalleryImage>();

    public ObservableLinkedList<GalleryImage> Images => _images;


}