using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityHub.Application.Database.Repositories;

namespace CommunityHub.Uix.Views;

public partial class LogInForm : Window
{
    private readonly UserDbRepository _userRepository;

    public LogInForm()
    {
        InitializeComponent();
        _userRepository = new UserDbRepository();
    }

    private void LoginButton_Click(object? sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text ?? "";
        string password = PasswordTextBox.Text ?? "";

        long? userId = _userRepository.GetIdByCredentials(username, password);

        if (userId != null)
        {
            HomeWindow homeWindow = new HomeWindow(userId.Value);
            homeWindow.Show();
            this.Close();
        }
        else
        {
            ErrorMessageTextBlock.Text = "Neispravno korisničko ime ili lozinka.";
            ErrorMessageTextBlock.IsVisible = true;
        }
    }
}
