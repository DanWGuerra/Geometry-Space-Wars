[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float timeSurvived;

    public LeaderboardEntry(string name, float time)
    {
        playerName = name;
        timeSurvived = time;
    }
}
