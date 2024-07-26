using UnityEngine;

public class CastingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float castingTime = 2f; // Duration of the casting
    private float timer; // Timer to track casting progress

    public void Enter(Player player)
    {
        Debug.Log("Entering Casting State");
        animator = player.GetComponent<Animator>();
        timer = castingTime;

        // Set the animator to play the casting animation
        animator.Play("Casting");

        // Optionally, disable certain controls during casting
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle casting logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Casting is complete; perform the spell or action
            CastSpell();
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Casting State");
        // Re-enable player controls or cleanup after casting
        player.enabled = true;
    }

    private void CastSpell()
    {
        // Implement the spell casting logic
        Debug.Log("Spell casted");
        // Example: Instantiate spell effects, apply effects to targets, etc.
    }
}

