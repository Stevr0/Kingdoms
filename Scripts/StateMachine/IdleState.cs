public class IdleState : IPlayerState
{
    public void Enter(Player player)
    {
        Debug.Log("Entering Idle State");
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Animator>().Play("Idle");
        // Possibly reset other parameters here
    }

    public void Execute(Player player)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.ChangeState(new RunningState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(new JumpingState());
        }
        // Additional checks can be added here
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Idle State");
        // Clean up or reset parameters if needed
    }

    public PlayerState GetStateType()
    {
        return PlayerState.Idle;
    }
}

