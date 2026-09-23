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
       
        PlayClickSound();

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
        Debug.Log("GoToMainMenu() called");
        PlayClickSound();

        fade.FadeToBlack(() =>
        {
            Debug.Log("Fade complete, loading Menu scene now, isFading = " + fade.isFading);
            SceneManager.LoadScene("Menu");
        });
    }


    private void PlayClickSound()
    {
        if (clickSfx != null)
            audioSource.PlayOneShot(clickSfx, clickVolume);
    }
}
