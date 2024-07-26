using UnityEngine;

public class HealingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float healingDuration = 2f; // Duration of the healing process
    private float timer; // Timer to track healing duration

    public void Enter(Player player)
    {
        Debug.Log("Entering Healing State");
        animator = player.GetComponent<Animator>();
        timer = healingDuration;

        // Play the healing animation
        animator.Play("Healing");

        // Optionally, disable player movement during healing
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle healing logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Complete the healing and transition to another state
            player.Heal(); // Perform the actual healing logic
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Healing State");
        // Re-enable player controls if they were disabled
        player.enabled = true;
    }
}

