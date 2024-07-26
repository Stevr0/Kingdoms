using UnityEngine;

public class ClimbingState : IPlayerState
{
    private Animator animator; // Animator component for player

    public void Enter(Player player)
    {
        Debug.Log("Entering Climbing State");
        animator = player.GetComponent<Animator>();

        // Set the animator to play the climbing animation
        animator.Play("Climbing");

        // Adjust player physics for climbing
        AdjustClimbingPhysics(player);
    }

    public void Execute(Player player)
    {
        // Handle climbing logic
        player.Climb();

        // Transition to Idle or other states based on conditions
        if (Input.GetKeyUp(KeyCode.Space)) // Example condition
        {
            player.ChangeState(new IdleState());
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Climbing State");
        // Revert player physics adjustments for climbing
        RevertClimbingPhysics(player);
    }

    private void AdjustClimbingPhysics(Player player)
    {
        // Implement logic to adjust physics for climbing
        // Example: Disable gravity, adjust collision detection, etc.
        player.GetComponent<Rigidbody>().useGravity = false;
    }

    private void RevertClimbingPhysics(Player player)
    {
        // Implement logic to revert physics adjustments
        player.GetComponent<Rigidbody>().useGravity = true;
    }
}

