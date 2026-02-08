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

    private void PlayClickSound()
    {
        if (clickSfx != null)
            audioSource.PlayOneShot(clickSfx, clickVolume);
    }
}
