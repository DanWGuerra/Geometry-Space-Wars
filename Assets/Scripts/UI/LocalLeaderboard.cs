using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LocalLeaderboard : MonoBehaviour
{
    private const string SaveKey = "LOCAL_LEADERBOARD";
    private const int MaxEntries = 5;

    public List<LeaderboardEntry> Entries { get; private set; } = new();

    private void Awake()
    {
        Load();
    }

    public bool Qualifies(float time)
    {
        if (Entries.Count < MaxEntries)
            return true;

        return time > Entries.Last().timeSurvived;
    }

    public void AddEntry(string name, float time)
    {
        Entries.Add(new LeaderboardEntry(name, time));

        Entries = Entries
            .OrderByDescending(e => e.timeSurvived)
            .Take(MaxEntries)
            .ToList();

        Save();
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(new Wrapper { entries = Entries });
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
            return;

        string json = PlayerPrefs.GetString(SaveKey);
        Entries = JsonUtility.FromJson<Wrapper>(json).entries;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<LeaderboardEntry> entries;
    }
}
