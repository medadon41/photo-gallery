using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using PhotoGallery.Commands;
using PhotoGallery.DataTypes;
using PhotoGallery.Models;
using PhotoGallery.Services;

namespace PhotoGallery.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public ObservableLinkedList<GalleryImage> Images;

    private LinkedListNode<GalleryImage>? _currentNode;

    private BitmapImage? _currentImage;

    public BitmapImage? CurrentImage
    {
        get => _currentImage;
        set
        {
            _currentImage = value;
            OnPropertyChanged("CurrentImage");
        }
    }

    private string? _description;

    public string? Description
    {
        get => CurrentNode?.Value.Description;
        set
        {
            CurrentNode.Value.Description = value;
            OnPropertyChanged("Description");
        }
    }

    public MainWindowViewModel(ObservableLinkedList<GalleryImage> images)
    {
        Images = images;
        CurrentNode = Images.First;
        CurrentImage = _currentNode?.Value.Image;
    }

    public MainWindowViewModel()
    {
        Images = new ObservableLinkedList<GalleryImage>();
        CurrentImage = null;
        _currentNode = null;
    }

    public LinkedListNode<GalleryImage>? CurrentNode
    {
        get => _currentNode;
        set
        {
            _currentNode = value;
            Description = value?.Value.Description;
            CurrentImage = value?.Value.Image;
            IsTextBoxEnabled = CurrentImage != null;
        }
    }

    public void Next()
    {
        CurrentNode = CurrentNode?.Next ?? Images.First;
    }

    public void Previous()
    {
        CurrentNode = CurrentNode?.Previous ?? Images.Last;
    }

    public void RemoveCurrent()
    {
        Images.Remove(CurrentNode);
        Next();
    }

    public void Add()
    {
        var images = FileManager.LoadFiles();

        if (images == null)
            return;

        foreach(GalleryImage image in images)
            Images.AddLast(image);

        CurrentNode = Images.Last;
    }

    public void Save()
    {
        FileManager.SaveFile(Images);
    }

    private bool _isTextBoxEnabled;
   
    public bool IsTextBoxEnabled
    {
        get => _isTextBoxEnabled;
        set
        {
            _isTextBoxEnabled = value;
            OnPropertyChanged("IsTextBoxEnabled");
        }
    }

    private Command _nextCommand;
    public Command NextCommand
    {
        get
        {
            return _nextCommand ??
                   (_nextCommand = new Command(obj =>
                   {
                       this.Next();
                   },
                       (obj) => Images.Count > 1));
        }
    }

    private Command _previousCommand;
    public Command PreviousCommand
    {
        get
        {
            return _previousCommand ??
                   (_previousCommand = new Command(obj =>
                   {
                       this.Previous();
                   },
                       (obj) => Images.Count > 1));
        }
    }

    private Command _removeCommand;
    public Command RemoveCommand
    {
        get
        {
            return _removeCommand ??
                   (_removeCommand = new Command(obj =>
                   {
                       this.RemoveCurrent();
                   },
                       (obj) => CurrentImage != null));
        }
    }

    private Command _addCommand;
    public Command AddCommand
    {
        get
        {
            return _addCommand ??
                   (_addCommand = new Command(obj =>
                   {
                       this.Add();
                   }));
        }
    }

    private Command _saveCommand;
    public Command SaveCommand
    {
        get
        {
            return _saveCommand ??
                   (_saveCommand = new Command(obj =>
                   {
                       this.Save();
                   }));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}