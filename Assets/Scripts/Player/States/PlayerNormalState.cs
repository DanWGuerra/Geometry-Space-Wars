public class PlayerNormalState : IPlayerState
{
    private readonly PlayerController player;

    public PlayerNormalState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter() { }

    public void Update()
    {
        player.HandleMovement();
        player.HandleShooting();
    }

    public void Exit() { }
}
