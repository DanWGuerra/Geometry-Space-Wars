using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private InputAction PauseButton;


    private void OnEnable()
    {
        PauseButton?.Enable();
    }

    private void OnDisable()
    {
        PauseButton?.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (PauseButton.WasPressedThisFrame())
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = (PauseCanvas.activeInHierarchy) ? 1.0f : 0.0f;
        PauseCanvas.SetActive(!PauseCanvas.activeInHierarchy);
    }
}
