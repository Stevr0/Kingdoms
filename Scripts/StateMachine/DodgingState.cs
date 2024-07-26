using UnityEngine;

public class DodgingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float dodgeDuration = 0.5f; // Duration of the dodge action
    private float timer; // Timer to track dodge duration

    public void Enter(Player player)
    {
        Debug.Log("Entering Dodging State");
        animator = player.GetComponent<Animator>();
        timer = dodgeDuration;

        // Play the dodge animation
        animator.Play("Dodging");

        // Optionally, disable player movement during dodge
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle dodge logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Complete the dodge and transition to another state
            player.Dodge(); // Perform the actual dodge logic
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Dodging State");
        // Re-enable player controls if they were disabled
        player.enabled = true;
    }
}

