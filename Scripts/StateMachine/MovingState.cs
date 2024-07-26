public class MovingState : IPlayerState
{
    public void Enter(Player player)
    {
        Debug.Log("Entering Moving State");
        // Set animation, initialize movement, etc.
    }

    public void Execute(Player player)
    {
        // Movement logic
        player.Move();

        // Transition to other states based on conditions
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(new AttackingState());
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            player.ChangeState(new IdleState());
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Moving State");
        // Clean up moving state, if needed
    }
}
