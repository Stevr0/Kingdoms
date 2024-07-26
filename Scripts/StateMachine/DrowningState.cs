using UnityEngine;

public class DrowningState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float drowningTime = 10f; // Time after which player starts taking damage due to drowning
    private float damageInterval = 1f; // Time between each damage tick
    private float damageTimer; // Timer to track damage intervals
    private float drowningTimer; // Timer to track overall drowning time
    private bool isDrowning = false; // Flag to check if drowning started

    public void Enter(Player player)
    {
        Debug.Log("Entering Drowning State");
        animator = player.GetComponent<Animator>();
        drowningTimer = drowningTime;
        damageTimer = damageInterval;

        // Play drowning animation
        animator.Play("Drowning");

        // Optionally, disable player movement during drowning
        player.enabled = false;

        isDrowning = true;
    }

    public void Execute(Player player)
    {
        if (isDrowning)
        {
            // Update timers
            drowningTimer -= Time.deltaTime;
            damageTimer -= Time.deltaTime;

            if (drowningTimer <= 0f)
            {
                // Handle player death due to drowning
                player.TakeDamage(); // Apply damage
                player.HandleDeath(); // Handle player death logic
                player.ChangeState(new DeadState()); // Transition to DeadState
                isDrowning = false;
            }
            else if (damageTimer <= 0f)
            {
                // Apply periodic damage to the player
                player.TakeDamage(); // Apply damage
                damageTimer = damageInterval; // Reset damage timer
            }
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Drowning State");
        // Re-enable player controls if they were disabled
        player.enabled = true;

        // Stop drowning animation or transition to another state if needed
        animator.Play("Idle");
    }
}
