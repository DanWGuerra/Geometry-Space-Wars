using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction ShootAction;

    private void OnEnable()
    {
        moveAction.Enable();
        ShootAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        ShootAction.Disable();
    }


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
        Vector2 movementInput = moveAction.ReadValue<Vector2>();

        
       

        Vector2 input = new Vector2(
            movementInput.x,
            0
        );

        movement.Move(input);
    }

    public void HandleShooting()
    {
        if (!ShootAction.IsPressed())
            return;

        if (heatSystem.IsOverheated)
            return;

        if (weapon.Fire())
        {
            heatSystem.AddHeat();
        }
    }
}
