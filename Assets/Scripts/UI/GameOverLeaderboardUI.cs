using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class GameOverLeaderboardUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LocalLeaderboard leaderboard;
    [SerializeField] private SurvivalTime timer;

    [Header("UI")]
    [SerializeField] private GameObject nameInputPanel, ButtonsPanel;
    [SerializeField] private TMP_InputField nameInput;

    private float finalTime;
    private bool awaitingSubmission;

    [SerializeField] private AudioClip clickSfx;
    [SerializeField] private float clickVolume = 1f;

    private AudioSource audioSource;


    private void Awake()
    {
        finalTime = timer.ElapsedTime;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (leaderboard.Qualifies(finalTime))
        {
            awaitingSubmission = true;
            
            nameInputPanel.SetActive(true);
            ButtonsPanel.SetActive(false);
            nameInput.text = "";
            nameInput.ActivateInputField();
        }
        else
        {
            awaitingSubmission = false;
            
            
        }
    }

    public void SubmitName()
    {
        if (!awaitingSubmission)
            return;

        awaitingSubmission = false;

        string playerName = string.IsNullOrWhiteSpace(nameInput.text)
            ? "AAAAA"
            : nameInput.text.Trim();

        leaderboard.AddEntry(playerName, finalTime);

        nameInputPanel.SetActive(false);
        PlayClickSound();
    }

    private void PlayClickSound()
    {
        if (clickSfx != null)
            audioSource.PlayOneShot(clickSfx, clickVolume);
    }
}
