using System.Text.Json;
using Azor.Web.Models.Entities;

namespace Azor.Web.Services;

public class SeasonStorage : ISeasonStorage
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "seasons.json");
    private List<Season> _seasons;
    private int _nextId;

    public SeasonStorage()
    {
        Load();
    }

    private void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _seasons = JsonSerializer.Deserialize<List<Season>>(json) ?? new List<Season>();
        }
        else
        {
            _seasons = new List<Season>();
        }
        _nextId = _seasons.Any() ? _seasons.Max(s => s.Id) + 1 : 1;
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_seasons, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<Season> GetAll() => _seasons.ToList();

    public Season GetById(int id) => _seasons.FirstOrDefault(s => s.Id == id);

    public void Add(Season season)
    {
        season.Id = _nextId++;
        _seasons.Add(season);
        Save();
    }

    public void Update(Season season)
    {
        var index = _seasons.FindIndex(s => s.Id == season.Id);
        if (index >= 0)
        {
            _seasons[index] = season;
            Save();
        }
    }

    public void Delete(int id)
    {
        var season = GetById(id);
        if (season != null)
        {
            _seasons.Remove(season);
            Save();
        }
    }
}