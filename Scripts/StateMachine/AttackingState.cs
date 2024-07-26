using UnityEngine;

public class AttackingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private float attackDuration = 1f; // Duration of the attack animation
    private float timer; // Timer to track attack duration

    public void Enter(Player player)
    {
        Debug.Log("Entering Attacking State");
        animator = player.GetComponent<Animator>();
        timer = attackDuration;

        // Play the attack animation
        animator.Play("Attack");

        // Optionally, disable player controls during the attack
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle attack logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Complete the attack and transition to another state
            player.Attack(); // Perform the actual attack logic
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Attacking State");
        // Re-enable player controls if they were disabled
        player.enabled = true;
    }
}

