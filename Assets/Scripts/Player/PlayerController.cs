using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HeatSystem))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private HeatSystem heatSystem;
    private IWeapon weapon;

    private IPlayerState currentState;
    private IPlayerState normalState;
    private IPlayerState overheatedState;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        heatSystem = GetComponent<HeatSystem>();
        weapon = GetComponent<IWeapon>();

        normalState = new PlayerNormalState(this);
        overheatedState = new PlayerOverheatedState(this);

        heatSystem.OnOverheat += () => SwitchState(overheatedState);
    }

    private void Start()
    {
        SwitchState(normalState);

    }

    private void Update()
    {
        currentState.Update();

        if (!heatSystem.IsOverheated && currentState == overheatedState)
            SwitchState(normalState);
    }

    private void SwitchState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    //States

    public void HandleMovement()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        movement.Move(input);
    }

    public void HandleShooting()
    {
        if (!Input.GetButton("Fire1")) return;
        if (heatSystem.IsOverheated)
            return;

        weapon.Fire();
        heatSystem.AddHeat();
    }
}
