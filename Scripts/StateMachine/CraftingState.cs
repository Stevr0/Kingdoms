using UnityEngine;

public class CraftingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private CraftingManager craftingManager; // Reference to the crafting manager
    private float craftingDuration = 2f; // Duration of the crafting
    private float timer; // Timer to track crafting progress

    public void Enter(Player player)
    {
        Debug.Log("Entering Crafting State");
        animator = player.GetComponent<Animator>();
        timer = craftingDuration;
        craftingManager = player.GetComponent<CraftingManager>();

        // Play the crafting animation
        animator.Play("Crafting");

        // Optionally, disable player controls during crafting
        player.enabled = false;
    }

    public void Execute(Player player)
    {
        // Handle crafting logic and countdown timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Complete the crafting process
            craftingManager.CraftItem(); // Perform the crafting logic
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Crafting State");
        // Re-enable player controls if they were disabled
        player.enabled = true;
    }
}
