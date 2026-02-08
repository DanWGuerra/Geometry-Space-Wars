public class PlayerOverheatedState : IPlayerState
{
    private readonly PlayerController player;

    public PlayerOverheatedState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // visual/audio feedback hook
    }

    public void Update()
    {
        player.HandleMovement(); // movement allowed, shooting disabled
    }

    public void Exit() { }
}
