using UnityEngine;

public class InteractingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float interactionDuration = 1.5f; // Duration of the interaction action
    private float timer; // Timer to track interaction duration

    public void Enter(Player player)
    {
        Debug.Log("Entering Interacting State");
        animator = player.GetComponent<Animator>();
        timer = interactionDuration;

        // Play the interaction animation
        animator.Play("Interacting");

        // Optionally, disable player movement during interaction
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle interaction logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Complete the interaction and transition to another state
            player.Interact(); // Perform the actual interaction logic
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Interacting State");
        // Re-enable player controls if they were disabled
        player.enabled = true;
    }
}

