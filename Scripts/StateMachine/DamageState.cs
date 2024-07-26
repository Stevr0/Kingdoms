using UnityEngine;

public class DamagedState : IPlayerState
{
	private Animator animator; // Animator component for player
	private float damageDuration = 2f; // Duration of the damaged state
	private float timer; // Timer to track damage duration

	public void Enter(Player player)
	{
		Debug.Log("Entering Damaged State");
		animator = player.GetComponent<Animator>();
		timer = damageDuration;

		// Set the animator to play the damaged animation
		animator.Play("Damaged");

		// Optionally apply damage effects
		// For example: player.ApplyDamageEffects();
	}

	public void Execute(Player player)
	{
		// Handle damage effects and countdown timer
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			// Transition back to normal state after damage effect duration
			player.ChangeState(new IdleState()); // or another state based on player's condition
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Damaged State");
		// Optionally reset or remove damage effects
	}
}
