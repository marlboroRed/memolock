using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using memolock.Models;
using memolock.Services;
using memolock.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace memolock.ViewModels; // 네임스페이스 확인

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    public ObservableCollection<Memo> Memos { get; } = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    private string _currentSortOrder = "생성일";
    private bool _isFavoritesOnly = false;

    public MainViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    // LoadMemosAsync는 페이지가 나타날 때 호출될 것이므로 생성자에서 직접 호출하지 않습니다.

    [RelayCommand]
    public async Task LoadMemosAsync()
    {
        Memos.Clear();
        var allMemos = await _databaseService.GetMemosAsync();

        var filteredMemos = string.IsNullOrWhiteSpace(SearchText)
            ? allMemos
            : allMemos.Where(m => m.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        filteredMemos = filteredMemos.Where(m => !m.IsHidden);

        if (_isFavoritesOnly)
        {
            filteredMemos = filteredMemos.Where(m => m.IsFavorite);
        }

        IOrderedEnumerable<Memo> sortedMemos;
        switch (_currentSortOrder)
        {
            case "가나다":
                sortedMemos = filteredMemos.OrderBy(m => m.Title);
                break;
            case "수정일":
                sortedMemos = filteredMemos.OrderByDescending(m => m.ModifiedDate);
                break;
            default:
                sortedMemos = filteredMemos.OrderByDescending(m => m.CreationDate);
                break;
        }

        foreach (var memo in sortedMemos.OrderByDescending(m => m.IsPinned))
        {
            Memos.Add(memo);
        }
    }

    [RelayCommand]
    private async Task SearchAsync() => await LoadMemosAsync();

    [RelayCommand]
    private async Task ToggleFavoritesFilterAsync()
    {
        _isFavoritesOnly = !_isFavoritesOnly;
        await LoadMemosAsync();
    }

    [RelayCommand]
    private async Task SortAsync()
    {
        var action = await Shell.Current.DisplayActionSheet("정렬", "취소", null, "생성일", "수정일", "가나다");
        if (!string.IsNullOrEmpty(action) && action != "취소")
        {
            _currentSortOrder = action;
            await LoadMemosAsync();
        }
    }

    [RelayCommand]
    private async Task GoToSettingsAsync() => await Shell.Current.GoToAsync(nameof(SettingsPage));

    [RelayCommand]
    private async Task CreateMemoAsync() => await Shell.Current.GoToAsync(nameof(MemoEditPage));

    [RelayCommand]
    private async Task SelectMemoAsync(Memo memo)
    {
        if (memo == null) return;
        await Shell.Current.GoToAsync($"{nameof(MemoDetailPage)}?Id={memo.Id}");
    }

    [RelayCommand]
    private async Task ToggleFavoriteAsync(Memo memo)
    {
        if (memo == null) return;
        memo.IsFavorite = !memo.IsFavorite;
        await _databaseService.SaveMemoAsync(memo);
        await LoadMemosAsync();
    }

    [RelayCommand]
    private async Task TogglePinAsync(Memo memo)
    {
        if (memo == null) return;
        memo.IsPinned = !memo.IsPinned;
        await _databaseService.SaveMemoAsync(memo);
        await LoadMemosAsync();
    }
}