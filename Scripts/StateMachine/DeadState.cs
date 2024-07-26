using UnityEngine;

public class DeadState : IPlayerState
{
	private Animator animator; // Animator component for player
	private Player player; // Reference to the player

	public void Enter(Player player)
	{
		Debug.Log("Entering Dead State");
		this.player = player;
		animator = player.GetComponent<Animator>();

		// Set the animator to play the death animation
		animator.Play("Death");

		// Disable player controls
		player.enabled = false;
	}

	public void Execute(Player player)
	{
		// Dead state typically has no regular updates, but you might want to handle respawning or other logic here
		// For example, checking if the player should respawn
		if (/* condition to respawn */)
		{
			player.ChangeState(new IdleState()); // Change state to Idle or another state after respawn logic
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Dead State");
		// Optionally re-enable player controls when exiting the dead state
		// player.enabled = true; // Uncomment if you want to re-enable player controls
	}
}

