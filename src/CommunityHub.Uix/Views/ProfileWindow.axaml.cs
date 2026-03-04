using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityHub.Application.Database.Repositories;
using CommunityHub.Application.Domain;

namespace CommunityHub.Uix.Views;

public partial class ProfileWindow : Window
{
    private readonly UserDbRepository _userRepository;

    private readonly long _userId;
    private User _user = null!;
    private ObservableCollection<Post> _posts = null!;

    public ProfileWindow(long userId)
    {
        InitializeComponent();
        _userId = userId;
        _userRepository = new UserDbRepository();
        LoadUser();
    }

    private void LoadUser()
    {
        User? user = _userRepository.GetWithPosts(_userId);
        if (user == null)
        {
            ErrorMessageText.Text = "Korisnik nije pronađen.";
            ErrorOverlay.IsVisible = true;
            return;
        }

        _user = user;
        UserInfoTextBlock.Text = $"{_user.Name} {_user.Surname} ({_user.BirthDay.ToString("dd.MM.yyyy.")})";

        _posts = new ObservableCollection<Post>(_user.Posts ?? []);
        PostsItemsControl.ItemsSource = _posts;
    }

    private async void NewPostButton_Click(object? sender, RoutedEventArgs e)
    {
        CreatePostWindow createPostWindow = new CreatePostWindow(_userId);
        bool result = await createPostWindow.ShowDialog<bool>(this);
        if (result && createPostWindow.CreatedPost != null)
        {
            _user.AddPost(createPostWindow.CreatedPost);
            _posts.Add(createPostWindow.CreatedPost);
        }
    }

    private void HomeButton_Click(object? sender, RoutedEventArgs e)
    {
        HomeWindow homeWindow = new HomeWindow(_userId);
        homeWindow.Show();
        this.Close();
    }

    private void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        this.Close();
    }

    private void ErrorOkButton_Click(object? sender, RoutedEventArgs e)
    {
        ErrorOverlay.IsVisible = false;
        this.Close();
    }
}
