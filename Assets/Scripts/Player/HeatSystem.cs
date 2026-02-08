using UnityEngine;
using System;

public class HeatSystem : MonoBehaviour
{
    [SerializeField] private float maxHeat = 100f;
    [SerializeField] private float heatPerShot = 2f;
    [SerializeField] private float cooldownRate = 30f;

    public float Heat01 => currentHeat / maxHeat;
    public bool IsOverheated { get; private set; }

    public event Action OnOverheat;
    public event Action<float> OnHeatChanged;

    private float currentHeat;

    public void AddHeat()
    {
        if (IsOverheated)
            return;

        currentHeat = Mathf.Clamp(currentHeat + heatPerShot, 0, maxHeat);
        OnHeatChanged?.Invoke(Heat01);

        if (currentHeat >= maxHeat)
            TriggerOverheat();
    }

    private void Update()
    {
        Cooldown();
    }

    private void Cooldown()
    {
        if (currentHeat <= 0f)
        {
            if (IsOverheated)
                Recover();

            return;
        }

        currentHeat = Mathf.Clamp(
            currentHeat - cooldownRate * Time.deltaTime,
            0,
            maxHeat
        );

        OnHeatChanged?.Invoke(Heat01);
    }

    private void TriggerOverheat()
    {
        IsOverheated = true;
        OnOverheat?.Invoke();
    }

    private void Recover()
    {
        IsOverheated = false;
        OnHeatChanged?.Invoke(Heat01);
    }
}
