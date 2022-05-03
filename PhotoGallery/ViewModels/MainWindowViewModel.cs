using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using PhotoGallery.Commands;
using PhotoGallery.DataTypes;
using PhotoGallery.Models;

namespace PhotoGallery.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private ProgramFile openedFile;

    private ObservableLinkedList<GalleryImage> _images;

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

    public MainWindowViewModel(ProgramFile file)
    {
        openedFile = file;
        _images = file.Images;
        _currentNode = _images.First;
    }

    public MainWindowViewModel(ObservableLinkedList<GalleryImage> images)
    {
        _images = images;
        CurrentNode = _images.First;
        CurrentImage = _currentNode?.Value.Image;
    }

    public MainWindowViewModel()
    {
        _images = new ObservableLinkedList<GalleryImage>();
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
        CurrentNode = CurrentNode?.Next ?? _images.First;
    }

    public void Previous()
    {
        CurrentNode = CurrentNode?.Previous ?? _images.Last;
    }

    public void RemoveCurrent()
    {
        _images.Remove(CurrentNode);
        Next();
    }

    public void Add()
    {
        var images = FileManager.LoadFiles();
        foreach(GalleryImage image in images)
            _images.AddLast(image);

        CurrentNode = _images.Last;
    }

    public void Save()
    {
        FileManager.SaveFile(_images);
    }

    private bool _isTextBoxEnabled;
   
    public bool IsTextBoxEnabled
    {
        get => _isTextBoxEnabled;
        set
        {
            if (_isTextBoxEnabled == value)
                return;

            _isTextBoxEnabled = value;
            OnPropertyChanged("IsTextBoxEnabled");
        }
    }

    private NavigateCommand _nextCommand;
    public NavigateCommand NextCommand
    {
        get
        {
            return _nextCommand ??
                   (_nextCommand = new NavigateCommand(obj =>
                   {
                       this.Next();
                   },
                       (obj) => _images.Count > 1));
        }
    }

    private NavigateCommand _previousCommand;
    public NavigateCommand PreviousCommand
    {
        get
        {
            return _previousCommand ??
                   (_previousCommand = new NavigateCommand(obj =>
                   {
                       this.Previous();
                   },
                       (obj) => _images.Count > 1));
        }
    }

    private NavigateCommand _removeCommand;
    public NavigateCommand RemoveCommand
    {
        get
        {
            return _removeCommand ??
                   (_removeCommand = new NavigateCommand(obj =>
                   {
                       this.RemoveCurrent();
                   },
                       (obj) => CurrentImage != null));
        }
    }

    private NavigateCommand _addCommand;
    public NavigateCommand AddCommand
    {
        get
        {
            return _addCommand ??
                   (_addCommand = new NavigateCommand(obj =>
                   {
                       this.Add();
                   }));
        }
    }

    private NavigateCommand _saveCommand;
    public NavigateCommand SaveCommand
    {
        get
        {
            return _saveCommand ??
                   (_saveCommand = new NavigateCommand(obj =>
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