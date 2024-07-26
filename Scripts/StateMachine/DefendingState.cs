using UnityEngine;

public class DefendingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float defenseDuration = 2f; // Duration of the defense action
    private float timer; // Timer to track defense duration

    public void Enter(Player player)
    {
        Debug.Log("Entering Defending State");
        animator = player.GetComponent<Animator>();
        timer = defenseDuration;

        // Play the defense animation
        animator.Play("Defending");

        // Optionally, disable player movement during defense
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle defense logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Complete the defense and transition to another state
            player.Defend(); // Perform the actual defense logic
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Defending State");
        // Re-enable player controls if they were disabled
        player.enabled = true;
    }
}

