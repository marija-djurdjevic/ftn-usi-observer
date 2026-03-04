using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CommunityHub.Uix.Views;

public partial class HomeWindow : Window
{
    private readonly long _userId;

    public HomeWindow(long userId)
    {
        InitializeComponent();
        _userId = userId;
    }

    private void ProfileButton_Click(object? sender, RoutedEventArgs e)
    {
        ProfileWindow profileWindow = new ProfileWindow(_userId);
        profileWindow.Show();
        this.Close();
    }

    private void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        this.Close();
    }
}
