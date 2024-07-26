using UnityEngine;

public class JumpingState : IPlayerState
{
	private float jumpForce = 10f; // Force applied for jumping
	private float gravityMultiplier = 2f; // Multiplier to increase gravity during the jump
	private Animator animator; // Animator component for player
	private Rigidbody rb; // Rigidbody component for player
	private bool hasJumped = false; // Flag to check if the jump has started

	public void Enter(Player player)
	{
		Debug.Log("Entering Jumping State");
		animator = player.GetComponent<Animator>();
		rb = player.GetComponent<Rigidbody>();

		// Set the animator to play the jumping animation
		animator.Play("Jump");

		// Apply initial jump force
		ApplyJumpForce(player);

		// Adjust gravity for a more realistic jump
		rb.gravityScale *= gravityMultiplier; // Adjust as needed
	}

	public void Execute(Player player)
	{
		// Transition to other states based on conditions
		if (player.IsGrounded())
		{
			// Transition back to idle or running when the player lands
			if (hasJumped)
			{
				player.ChangeState(new IdleState()); // or RunningState based on player's input
			}
		}
		else if (player.IsSwimmingOnSurface()) // Check if the player should be swimming
		{
			player.ChangeState(new SwimmingState());
		}
		else if (player.IsDead()) // Check if the player is dead
		{
			player.ChangeState(new DeadState());
		}
	}

	public void Exit(Player player)
	{
		Debug.Log("Exiting Jumping State");
		// Reset gravity to normal when exiting the jumping state
		rb.gravityScale /= gravityMultiplier; // Adjust as needed

		// Reset animator state if needed
		animator.Play("Idle");
	}

	private void ApplyJumpForce(Player player)
	{
		if (rb != null)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			hasJumped = true;
		}
	}
}
