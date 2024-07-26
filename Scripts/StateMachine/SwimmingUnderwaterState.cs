using UnityEngine;

public class SwimmingUnderwaterState : IPlayerState
{
    private Rigidbody rb; // Rigidbody component for physics
    private Animator animator; // Animator component for player
    private float swimSpeed = 5f; // Speed at which the player swims underwater
    private float buoyancy = 2f; // Buoyancy force for staying afloat underwater

    public void Enter(Player player)
    {
        Debug.Log("Entering Swimming Underwater State");
        rb = player.GetComponent<Rigidbody>();
        animator = player.GetComponent<Animator>();

        // Play the underwater swimming animation
        animator.Play("SwimmingUnderwater");

        // Optionally, adjust physics for underwater swimming
        if (rb != null)
        {
            rb.drag = 2f; // Increase drag for slower movement
            rb.angularDrag = 2f; // Increase angular drag for more control
        }
    }

    public void Execute(Player player)
    {
        // Handle underwater swimming logic
        MoveUnderwater(player);

        // Transition to other states based on conditions
        if (Input.GetKeyDown(KeyCode.Space)) // Example condition for transitioning
        {
            player.ChangeState(new SwimmingState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Swimming Underwater State");
        // Reset physics adjustments if needed
        if (rb != null)
        {
            rb.drag = 1f; // Reset drag to default
            rb.angularDrag = 1f; // Reset angular drag to default
        }
    }

    private void MoveUnderwater(Player player)
    {
        // Example movement logic for underwater swimming
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.transform.Translate(movement * swimSpeed * Time.deltaTime, Space.World);
    }
}

