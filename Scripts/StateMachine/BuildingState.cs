using UnityEngine;

public class BuildingState : IPlayerState
{
    private Animator animator; // Animator component for player
    private GameObject buildingPreview; // Preview of the building to be placed

    public void Enter(Player player)
    {
        Debug.Log("Entering Building State");
        animator = player.GetComponent<Animator>();

        // Set the animator to play the building animation
        animator.Play("Building");

        // Create or activate building preview
        buildingPreview = Instantiate(Resources.Load("BuildingPreviewPrefab") as GameObject);
        buildingPreview.SetActive(true);
    }

    public void Execute(Player player)
    {
        // Handle building placement logic
        Vector3 placementPosition = GetPlacementPosition(); // Implement this method to get placement position
        buildingPreview.transform.position = placementPosition;

        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for placement
        {
            PlaceBuilding(placementPosition);
            player.ChangeState(new IdleState()); // or another appropriate state
        }
    }

    public void Exit(Player player)
    {
        Debug.Log("Exiting Building State");
        // Clean up building preview
        if (buildingPreview != null)
        {
            Object.Destroy(buildingPreview);
        }
    }

    private Vector3 GetPlacementPosition()
    {
        // Implement logic to determine where the building should be placed
        // For example, raycasting from the camera to the mouse position
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void PlaceBuilding(Vector3 position)
    {
        // Implement logic to place the building at the specified position
        Debug.Log("Building placed at: " + position);
        // Instantiate or enable the actual building object
    }
}

