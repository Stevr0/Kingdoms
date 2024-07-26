using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float jumpForce = 5f; // Force of the jump
    public float swimSpeed = 3f; // Speed when swimming
    public LayerMask groundLayer; // Layer for ground detection

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!IsOwner) return; // Ensure that only the local player controls their character

        HandleMovement();
        HandleJump();
        HandleSwimming();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = transform.TransformDirection(movement);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleSwimming()
    {
        if (IsSwimming())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical);
            rb.MovePosition(rb.position + movement * swimSpeed * Time.deltaTime);
        }
    }

    bool IsSwimming()
    {
        // Implement swimming detection logic, e.g., checking if the player is in water
        return false; // Placeholder
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((groundLayer & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if ((groundLayer & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = false;
        }
    }
}
