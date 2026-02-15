using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LocalLeaderboard leaderboard;
    [SerializeField] private TMP_Text leaderboardText;

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        leaderboardText.text = "";

        for (int i = 0; i < leaderboard.Entries.Count; i++)
        {
            var entry = leaderboard.Entries[i];
            leaderboardText.text +=
                $"{i + 1}. {entry.playerName} - {entry.timeSurvived:0.0}s\n";
        }
    }
}
