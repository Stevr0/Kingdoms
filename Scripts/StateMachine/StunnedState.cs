using UnityEngine;

public class StunnedState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float stunDuration = 3f; // Duration of the stun effect
    private float timer; // Timer to track stun duration

    public void Enter(Player player)
    {
        Debug.Log("Entering Stunned State");
        animator = player.GetComponent<Animator>();
        timer = stunDuration;

        // Set the animator to play the stunned animation
        animator.Play("Stunned");

        // Disable player controls or limit actions
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle stun effect and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Transition back to normal state or another state
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Stunned State");
        // Re-enable player controls or remove stun effects
        player.enabled = true;
    }
}

