using UnityEngine;
using UnityEngine.UI;

public class HeatUI : MonoBehaviour
{
    [SerializeField] private HeatSystem heatSystem;
    [SerializeField] private Image heatFillImage;

    [SerializeField] private Color coolColor = Color.cyan;
    [SerializeField] private Color hotColor = Color.yellow;
    [SerializeField] private Color overheatColor = Color.red;

    private void OnEnable()
    {
        heatSystem.OnHeatChanged += UpdateHeat;
        heatSystem.OnOverheat += HandleOverheat;
    }

    private void OnDisable()
    {
        heatSystem.OnHeatChanged -= UpdateHeat;
        heatSystem.OnOverheat -= HandleOverheat;
    }

    private void Start()
    {
        UpdateHeat(heatSystem.Heat01);
    }

    private void UpdateHeat(float heat01)
    {
        heatFillImage.fillAmount = heat01;

        if (heatSystem.IsOverheated)
        {
            heatFillImage.color = overheatColor;
            return;
        }

        heatFillImage.color = Color.Lerp(coolColor, hotColor, heat01);
    }

    private void HandleOverheat()
    {
        heatFillImage.color = overheatColor;
    }
}
