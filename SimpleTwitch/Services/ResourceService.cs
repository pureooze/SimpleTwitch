using System.Reflection;
using System.Text.Json;
using SimpleTwitch.Data;

namespace SimpleTwitch.Services;

public class ResourceService {

    private Assembly m_assembly;
    private const string FavsConfig = "SimpleTwitch.Resources.Json.fav.json";
    private const string GlobalBadges = "SimpleTwitch.Resources.Json.global-badges.json";
    
    private List<string>? m_favs;
    private Dictionary<string, GameInfo>? m_globalBadges;
    
    private readonly JsonSerializerOptions m_options = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public ResourceService() {
        m_assembly = GetType().Assembly;
    }

    public async Task<Dictionary<string, GameInfo>> GetGlobalBadges() {
        
        if (m_globalBadges is not null) {
            return m_globalBadges;
        }
        
        await using Stream? stream = m_assembly.GetManifestResourceStream(GlobalBadges);
        
        if (stream is null) {
            return [];
        }
        
        using StreamReader reader = new(stream);
        string json = await reader.ReadToEndAsync();
        
        m_globalBadges = JsonSerializer.Deserialize<Dictionary<string, GameInfo>>(json, m_options) ?? [];
        return m_globalBadges;
    }
    
    public async Task<List<string>> GetFavs() {
        
        if (m_favs is not null) {
            return m_favs;
        }
        
        await using Stream? stream = m_assembly.GetManifestResourceStream(FavsConfig);
        
        if (stream is null) {
            return [];
        }
        
        using StreamReader reader = new(stream);
        string json = await reader.ReadToEndAsync();
        
        m_favs = JsonSerializer.Deserialize<List<string>>(json) ?? [];
        return m_favs;
    }
}