using UnityEngine;

public class RunningState : IPlayerState
{
    private float runSpeed = 5f; // Speed at which the player runs
    private Animator animator; // Animator component for player
    private Rigidbody rb; // Rigidbody component for player

    public void Enter(Player player)
    {
        Debug.Log("Entering Running State");
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();

        // Set the animator to play the running animation
        animator.Play("Run");

        // Adjust physics for running if needed
        rb.drag = 0; // Example: Set drag to 0 to allow smooth running
    }

    public void Execute(Player player)
    {
        // Movement logic
        Move(player);

        // Transition to Idle or other states based on conditions
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.ChangeState(new IdleState());
        }
        else if (!player.IsGrounded()) // Check if the player is in the air
        {
            player.ChangeState(new JumpingState());
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
        Debug.Log("Exiting Running State");
        // Reset animator state if needed
        animator.Play("Idle");
        // Reset physics if needed
        rb.drag = 1; // Example: Reset drag to default
    }

    private void Move(Player player)
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical) * runSpeed * Time.deltaTime;

        // Apply movement to the player
        player.transform.Translate(movement, Space.World);

        // Optionally apply force to Rigidbody if using physics-based movement
        // rb.AddForce(movement, ForceMode.VelocityChange);
    }
}

