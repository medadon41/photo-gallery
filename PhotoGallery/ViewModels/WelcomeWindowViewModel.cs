using System.Windows;
using System.Windows.Input;
using PhotoGallery.Commands;
using PhotoGallery.Services;

namespace PhotoGallery.ViewModels;

public class WelcomeWindowViewModel
{
    public WelcomeWindowViewModel()
    {
        _createCommand = new Command(obj =>
        {
            CreateNew();
        });

        _openCommand = new Command(obj =>
        {
            Open();
        });

    }

    private Command _createCommand;

    public ICommand CreateCommand => _createCommand;

    private Command _openCommand;

    public Command OpenCommand => _openCommand;

    private void Open()
    {
        var images = FileManager.LoadFile();
        if (images != null)
        {
            MainWindow newWindow = new MainWindow(images);
            newWindow.Show();
            CloseWindow();
        }
    }

    private void CreateNew()
    {
        MainWindow newWindow = new MainWindow();
        newWindow.Show();
        CloseWindow();
    }

    private void CloseWindow()
    {
        foreach (Window item in Application.Current.Windows)
        {
            if (item.DataContext == this) item.Close();
        }
    }
}