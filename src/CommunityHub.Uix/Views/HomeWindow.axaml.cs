using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityHub.Application.Database.Repositories;

namespace CommunityHub.Uix.Views;

public partial class HomeWindow : Window
{
    private readonly long _userId;
    private readonly PostDbRepository _postRepository;

    public HomeWindow(long userId, PostDbRepository? postRepository = null)
    {
        InitializeComponent();
        _userId = userId;
        _postRepository = postRepository ?? new PostDbRepository();
    }

    private void ProfileButton_Click(object? sender, RoutedEventArgs e)
    {
        ProfileWindow profileWindow = new ProfileWindow(_userId, _postRepository);
        profileWindow.Show();
        Close();
    }

    private void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        Close();
    }
}