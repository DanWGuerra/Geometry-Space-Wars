using UnityEngine;
using TMPro;

public class SurvivalTimeUI : MonoBehaviour
{
    [SerializeField] private SurvivalTime timer;
    [SerializeField] private TMP_Text timeText;


    [SerializeField] private UIFadeTransition fade;
    void Start()
    {
        fade.FadeFromBlack();
    }



    private void OnEnable()
    {
        timer.OnTimeUpdated += UpdateText;
    }

    private void OnDisable()
    {
        timer.OnTimeUpdated -= UpdateText;
    }

    private void UpdateText(float time)
    {
        timeText.text = $"{time:0.0}";
    }
}
