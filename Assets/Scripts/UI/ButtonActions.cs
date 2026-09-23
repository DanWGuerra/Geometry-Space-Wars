using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] private UIFadeTransition fade;

    
    [SerializeField] private AudioClip clickSfx;
    [SerializeField] private float clickVolume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void StartGame()
    {
        Debug.Log("StartGame() called");
        PlayClickSound();

        if (fade == null)
        {
            Debug.LogError("Fade reference is NULL on this ButtonActions component!");
            return;
        }

        fade.FadeToBlack(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        });
    }

    public void QuitGame()
    {
        PlayClickSound();

        fade.FadeToBlack(() =>
        {
            Application.Quit();
        });
    }

    public void GoToMainMenu()
    {
        PlayClickSound();

        fade.FadeToBlack(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        });
    }


    private void PlayClickSound()
    {
        if (clickSfx != null)
            audioSource.PlayOneShot(clickSfx, clickVolume);
    }
}
