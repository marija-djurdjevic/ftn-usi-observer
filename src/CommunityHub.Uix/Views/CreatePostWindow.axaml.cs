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

    public Post? CreatedPost { get; private set; }

    public CreatePostWindow(long userId)
    {
        InitializeComponent();
        _userId = userId;
        _postRepository = new PostDbRepository();

        TitleTextBox.GetObservable(TextBox.TextProperty).Subscribe(new TextObserver(this));
    }

    private class TextObserver(CreatePostWindow owner) : IObserver<string?>
    {
        public void OnNext(string? value) => owner.OnTitleTextChanged(value);
        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }

    private void OnTitleTextChanged(string? text)
    {
        bool isValid = (text?.Length ?? 0) >= 3;
        SaveButton.IsEnabled = isValid;
        ValidationErrorTextBlock.IsVisible = (text?.Length ?? 0) > 0 && !isValid;
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        Post post = new Post(TitleTextBox.Text!, ContentTextBox.Text ?? "");
        CreatedPost = _postRepository.Create(post, _userId);

        Close(true);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
