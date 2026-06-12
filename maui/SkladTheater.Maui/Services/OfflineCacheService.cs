using SkladTheater.Maui.Models;

namespace SkladTheater.Maui.Services;

public interface IOfflineCacheService
{
    bool HasCachedData();
    void SaveStalls(List<StallDto> stalls);
    List<StallDto>? GetStalls();
    void SaveChests(List<Chest> chests);
    List<Chest>? GetChests();
    void SaveItems(List<ItemDto> items);
    List<ItemDto>? GetItems();
    void SaveTasks(List<TaskDto> tasks);
    List<TaskDto>? GetTasks();
    void SaveTapes(List<TapeDto> tapes, SeasonStats stats);
    (List<TapeDto>? Tapes, SeasonStats? Stats) GetTapes();
    void SaveShows(List<ShowDto> shows);
    List<ShowDto>? GetShows();
    void SaveKassety(List<KassetaDto> kassety);
    List<KassetaDto>? GetKassety();
    void SaveGrafik(string month, ScheduleMonth schedule);
    ScheduleMonth? GetGrafik(string month);
    void SaveUsers(List<UserDto> users);
    List<UserDto>? GetUsers();
    void SaveChats(List<ChatDto> chats);
    List<ChatDto>? GetChats();
    void SaveCurrentUser(UserDto user);
    UserDto? GetCurrentUser();
    void Clear();
}

public class OfflineCacheService : IOfflineCacheService
{
    private readonly IPreferenceService _prefs;

    private const string StallsKey = "cache_stalls";
    private const string ChestsKey = "cache_chests";
    private const string ItemsKey = "cache_items";
    private const string TasksKey = "cache_tasks";
    private const string TapesKey = "cache_tapes";
    private const string TapeStatsKey = "cache_tape_stats";
    private const string ShowsKey = "cache_shows";
    private const string KassetyKey = "cache_kassety";
    private const string GrafikPrefix = "cache_grafik_";
    private const string UsersKey = "cache_users";
    private const string ChatsKey = "cache_chats";
    private const string CurrentUserKey = "cache_user";

    public OfflineCacheService(IPreferenceService prefs)
    {
        _prefs = prefs;
    }

    public bool HasCachedData()
    {
        return _prefs.Contains(StallsKey)
            || _prefs.Contains(ChestsKey)
            || _prefs.Contains(ItemsKey)
            || _prefs.Contains(TasksKey)
            || _prefs.Contains(CurrentUserKey);
    }

    public void SaveStalls(List<StallDto> stalls) => SaveWithTime(StallsKey, stalls);
    public List<StallDto>? GetStalls() => GetWithTime<List<StallDto>>(StallsKey);

    public void SaveChests(List<Chest> chests) => SaveWithTime(ChestsKey, chests);
    public List<Chest>? GetChests() => GetWithTime<List<Chest>>(ChestsKey);

    public void SaveItems(List<ItemDto> items) => SaveWithTime(ItemsKey, items);
    public List<ItemDto>? GetItems() => GetWithTime<List<ItemDto>>(ItemsKey);

    public void SaveTasks(List<TaskDto> tasks) => SaveWithTime(TasksKey, tasks);
    public List<TaskDto>? GetTasks() => GetWithTime<List<TaskDto>>(TasksKey);

    public void SaveTapes(List<TapeDto> tapes, SeasonStats stats)
    {
        SaveWithTime(TapesKey, tapes);
        _prefs.SetObject(TapeStatsKey, stats);
    }
    public (List<TapeDto>? Tapes, SeasonStats? Stats) GetTapes()
    {
        var tapes = GetWithTime<List<TapeDto>>(TapesKey);
        var stats = _prefs.GetObject<SeasonStats>(TapeStatsKey);
        return (tapes, stats);
    }

    public void SaveShows(List<ShowDto> shows) => SaveWithTime(ShowsKey, shows);
    public List<ShowDto>? GetShows() => GetWithTime<List<ShowDto>>(ShowsKey);

    public void SaveKassety(List<KassetaDto> kassety) => SaveWithTime(KassetyKey, kassety);
    public List<KassetaDto>? GetKassety() => GetWithTime<List<KassetaDto>>(KassetyKey);

    public void SaveGrafik(string month, ScheduleMonth schedule) => SaveWithTime(GrafikPrefix + month, schedule);
    public ScheduleMonth? GetGrafik(string month) => GetWithTime<ScheduleMonth>(GrafikPrefix + month);

    public void SaveUsers(List<UserDto> users) => SaveWithTime(UsersKey, users);
    public List<UserDto>? GetUsers() => GetWithTime<List<UserDto>>(UsersKey);

    public void SaveChats(List<ChatDto> chats) => SaveWithTime(ChatsKey, chats);
    public List<ChatDto>? GetChats() => GetWithTime<List<ChatDto>>(ChatsKey);

    public void SaveCurrentUser(UserDto user) => SaveWithTime(CurrentUserKey, user);
    public UserDto? GetCurrentUser() => GetWithTime<UserDto>(CurrentUserKey);

    public void Clear()
    {
        _prefs.Remove(StallsKey);
        _prefs.Remove(ChestsKey);
        _prefs.Remove(ItemsKey);
        _prefs.Remove(TasksKey);
        _prefs.Remove(TapesKey);
        _prefs.Remove(TapeStatsKey);
        _prefs.Remove(ShowsKey);
        _prefs.Remove(KassetyKey);
        _prefs.Remove(UsersKey);
        _prefs.Remove(ChatsKey);
        _prefs.Remove(CurrentUserKey);
        // grafik keys remain intentionally
    }

    private void SaveWithTime<T>(string key, T value)
    {
        _prefs.SetObject(key, value);
        _prefs.Set(key + "_time", DateTime.UtcNow.ToString("O"));
    }

    private T? GetWithTime<T>(string key) where T : class
    {
        return _prefs.GetObject<T>(key);
    }
}
