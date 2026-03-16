using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityHub.Application.Database.Repositories;
using CommunityHub.Application.Domain;

namespace CommunityHub.Uix.Views;

public partial class CreatePostWindow : Window
{
    private readonly long _userId;
    private readonly PostDbRepository _postRepository;

    public CreatePostWindow(long userId, PostDbRepository postRepository)
    {
        InitializeComponent();
        _userId = userId;
        _postRepository = postRepository;
    }

    private void OnTitleTextBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        string? text = TitleTextBox.Text;

        bool isValid = (text?.Length ?? 0) >= 3;
        SaveButton.IsEnabled = isValid;
        ValidationErrorTextBlock.IsVisible = (text?.Length ?? 0) > 0 && !isValid;
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        Post post = new Post(TitleTextBox.Text!, ContentTextBox.Text ?? "");
        _postRepository.Create(post, _userId);
        Close();
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}