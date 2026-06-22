using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsUI : MonoBehaviour
{
     private Animator animator;
    [SerializeField] private GameObject InstructionsPanel;

    [SerializeField] private AudioClip clickSfx;
    [SerializeField] private float clickVolume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        animator = InstructionsPanel.GetComponent<Animator>();
    }



    public void BackMainMenuButton()
    {
        PlayClickSound();
        animator.Play("Back");
        Invoke("DeactivatePanel", 1);
    }

    private void DeactivatePanel()
    {
        InstructionsPanel.SetActive(false);
    }

    private void PlayClickSound()
    {
        if (clickSfx != null)
            audioSource.PlayOneShot(clickSfx, clickVolume);
    }
}
