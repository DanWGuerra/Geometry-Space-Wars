using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SetScreenSize : MonoBehaviour
{
    [SerializeField] private TMP_Text resolutionText;

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private GameObject SettingsPanel;

    [SerializeField] private AudioClip clickSfx;
    [SerializeField] private float clickVolume = 1f;

    private readonly Vector2Int[] resolutions =
    {
        new Vector2Int(480, 640),
        new Vector2Int(600, 800),
        new Vector2Int(768, 1024),
        new Vector2Int(900, 1200)
    };

    private int currentIndex;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        animator = SettingsPanel.GetComponent<Animator>();
    }

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("ResolutionIndex", 2);
        ApplyResolution();
    }

    public void Previous()
    {
        currentIndex = (currentIndex - 1 + resolutions.Length) % resolutions.Length;
        ApplyResolution();
        PlayClickSound();
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % resolutions.Length;
        ApplyResolution();
        PlayClickSound();
    }

    private void ApplyResolution()
    {
        Vector2Int res = resolutions[currentIndex];

        resolutionText.text = $"{res.x} x {res.y}";

        Screen.SetResolution(res.x, res.y, FullScreenMode.Windowed);

        PlayerPrefs.SetInt("ResolutionIndex", currentIndex);
        PlayerPrefs.Save();
    }
    public void BackMainMenuButton()
    {
        PlayClickSound();
        animator.Play("Back");
        Invoke("DeactivatePanel", 1);
    }

    private void DeactivatePanel()
    {
        SettingsPanel.SetActive(false);
    }

    private void PlayClickSound()
    {
        if (clickSfx != null)
            audioSource.PlayOneShot(clickSfx, clickVolume);
    }
}
