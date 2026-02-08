using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] SurvivalTime timer;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private float gameOverVolume = 1f;
    

    private void Awake()
    {
        gameOverCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        playerHealth.OnPlayerDied += ShowGameOver;
    }

    private void OnDisable()
    {
        playerHealth.OnPlayerDied -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        timer.StopTimer();
        gameOverCanvas.SetActive(true);
        SwapToGameOverMusic();
    }

    private void SwapToGameOverMusic()
    {
        if (musicSource == null || gameOverMusic == null)
            return;

        musicSource.Stop();
        musicSource.clip = gameOverMusic;
        musicSource.volume = gameOverVolume;
        musicSource.loop = true;
        musicSource.Play();
    }
}
