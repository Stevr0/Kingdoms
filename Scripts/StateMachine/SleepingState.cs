using UnityEngine;

public class SleepingState : IPlayerState
{
    private Animator animator; // Animator component for player

    public void Enter(Player player)
    {
        Debug.Log("Entering Sleeping State");
        animator = player.GetComponent<Animator>();

        // Set the animator to play the sleeping animation
        animator.Play("Sleep");

        // Disable player controls
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Sleeping state typically has no regular updates, but you might want to handle waking up logic here
        // For example, checking if the player should wake up
        if (/* condition to wake up */)
        {
            player.ChangeState(new IdleState()); // Change state to Idle or another state after waking up
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Sleeping State");
        // Re-enable player controls when exiting the sleeping state
        player.enabled = true;
    }
}

