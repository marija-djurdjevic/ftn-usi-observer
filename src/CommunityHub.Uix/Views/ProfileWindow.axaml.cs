using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityHub.Application.Database.Repositories;
using CommunityHub.Application.Domain;
using CommunityHub.Application.Observer;

namespace CommunityHub.Uix.Views;

public partial class ProfileWindow : Window, IObserver
{
    private readonly UserDbRepository _userRepository;
    private readonly PostDbRepository _postRepository;

    private readonly long _userId;
    private User _user = null!;
    public ObservableCollection<Post> Posts { get; set; }

    public ProfileWindow(long userId, PostDbRepository postRepository)
    {
        InitializeComponent();
        _userId = userId;
        _userRepository = new UserDbRepository();
        _postRepository = postRepository;

        Posts = new ObservableCollection<Post>();
        PostsItemsControl.ItemsSource = Posts;

        _postRepository.PostSubject.Subscribe(this);

        LoadUserInfo();
        Update();
    }

    private void LoadUserInfo()
    {
        User? user = _userRepository.GetWithPosts(_userId);
        if (user == null)
        {
            ErrorMessageText.Text = "Korisnik nije pronađen.";
            ErrorOverlay.IsVisible = true;
            return;
        }

        _user = user;
        UserInfoTextBlock.Text = $"{_user.Name} {_user.Surname} ({_user.BirthDay:dd.MM.yyyy.})";
    }

    public void Update()
    {
        User? user = _userRepository.GetWithPosts(_userId);
        if (user == null) return;

        _user = user;

        Posts.Clear();
        foreach (Post post in _user.Posts ?? [])
        {
            Posts.Add(post);
        }
    }

    private async void NewPostButton_Click(object? sender, RoutedEventArgs e)
    {
        CreatePostWindow createPostWindow = new CreatePostWindow(_userId, _postRepository);
        await createPostWindow.ShowDialog(this);
    }

    private void HomeButton_Click(object? sender, RoutedEventArgs e)
    {
        HomeWindow homeWindow = new HomeWindow(_userId, _postRepository);
        homeWindow.Show();
        Close();
    }

    private void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogInForm loginForm = new LogInForm();
        loginForm.Show();
        Close();
    }

    private void ErrorOkButton_Click(object? sender, RoutedEventArgs e)
    {
        ErrorOverlay.IsVisible = false;
        Close();
    }

    protected override void OnClosed(EventArgs e)
    {
        _postRepository.PostSubject.Unsubscribe(this);
        base.OnClosed(e);
    }
}