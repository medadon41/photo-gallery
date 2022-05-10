using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Microsoft.Win32;
using PhotoGallery.DataTypes;
using PhotoGallery.Models;

namespace PhotoGallery.Services;

public static class FileManager
{
    public static string LastFileName;
    public static ObservableLinkedList<GalleryImage>? LoadFiles()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog() == true)
        {
            ObservableLinkedList<GalleryImage> result = new ObservableLinkedList<GalleryImage>();
            foreach (string fileName in openFileDialog.FileNames)
            {
                result.AddLast( new GalleryImage
                {
                    Image = new BitmapImage(new Uri(fileName)),
                    Description = fileName
                });
            }
            LastFileName = openFileDialog.FileName;
            return result;
        }

        return null;
    }

    public static ObservableLinkedList<GalleryImage>? LoadFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "XML Files (*.xml*)|*.xml*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog() == true)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ByteGalleryImage>));

            ObservableLinkedList<GalleryImage> result = new ObservableLinkedList<GalleryImage>();
            List<ByteGalleryImage>? des;
            using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
            {
                des = formatter.Deserialize(fs) as List<ByteGalleryImage>;
            }

            foreach (var byteImage in des)
            {
                result.AddLast(ToGalleryImage(byteImage));
            }

            LastFileName = openFileDialog.FileName;
            return result;
        }

        return null;
    }

    public static void SaveFileAs(ObservableLinkedList<GalleryImage> images)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "XML Files (*.xml)|*.xml"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            List<ByteGalleryImage> result = new List<ByteGalleryImage>();

            foreach (GalleryImage image in images)
                result.Add(ToByteArray(image));


            XmlSerializer formatter = new XmlSerializer(typeof(List<ByteGalleryImage>));
            using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                formatter.Serialize(fs, result);
        }
    }

    public static void SaveFile(ObservableLinkedList<GalleryImage> images)
    {
        List<ByteGalleryImage> result = new List<ByteGalleryImage>();

        foreach (GalleryImage image in images)
            result.Add(ToByteArray(image));

        XmlSerializer formatter = new XmlSerializer(typeof(List<ByteGalleryImage>));
        using (FileStream fs = new FileStream(LastFileName, FileMode.OpenOrCreate))
            formatter.Serialize(fs, result);
    }

    private static ByteGalleryImage ToByteArray(GalleryImage image)
    {
        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        using (MemoryStream ms = new MemoryStream())
        {
            encoder.Frames.Add(BitmapFrame.Create(image.Image));
            
            encoder.Save(ms);
            var data = ms.ToArray();
            return new ByteGalleryImage
            {
                Image = data,
                Description = image.Description
            };
        }
    }

    private static GalleryImage ToGalleryImage(ByteGalleryImage byteImage)
    {
        using (MemoryStream ms = new MemoryStream(byteImage.Image))
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            
            return new GalleryImage
            {
                Image = image,
                Description = byteImage.Description
            };
        }
    }
}