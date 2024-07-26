using UnityEngine;

public class SwimmingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private Rigidbody rb; // Rigidbody component for player
    private float swimSpeed = 5f; // Speed of swimming

    public void Enter(Player player)
    {
        Debug.Log("Entering Swimming State");
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();

        // Set the animator to play the swimming animation
        animator.Play("Swim");

        // Adjust physics for swimming
        rb.drag = 2f; // Increase drag for slower swimming movement
        rb.angularDrag = 2f; // Increase angular drag for smoother rotation in water
    }

    public void Execute(Player player)
    {
        // Handle swimming movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical) * swimSpeed;
        rb.velocity = movement;

        // Transition to other states based on conditions
        if (player.IsGrounded())
        {
            player.ChangeState(new IdleState()); // or RunningState based on player's input
        }
        else if (player.IsDead())
        {
            player.ChangeState(new DeadState());
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Swimming State");
        // Reset physics for normal movement
        rb.drag = 0f;
        rb.angularDrag = 0f;
    }
}
