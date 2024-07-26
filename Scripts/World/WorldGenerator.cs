using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject landmassPrefab; // Prefab for landmasses
    public int numLandmasses = 100;
    public int numPlayers = 50;
    private List<GameObject> landmasses = new List<GameObject>();
    private List<int> playerLocations = new List<int>();

    void Start()
    {
        GenerateLandmasses();
        AssignPlayerLocations();
    }

    void GenerateLandmasses()
    {
        for (int i = 0; i < numLandmasses; i++)
        {
            Vector3 position = new Vector3(i * 10.0f, 0, 0); // Example positioning, adjust as needed
            GameObject landmass = Instantiate(landmassPrefab, position, Quaternion.identity);
            landmasses.Add(landmass);
        }
    }

    void AssignPlayerLocations()
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < numLandmasses; i++)
        {
            indices.Add(i);
        }

        Shuffle(indices);

        for (int i = 0; i < numLandmasses && playerLocations.Count < numPlayers; i += 2)
        {
            playerLocations.Add(indices[i]);
        }

        foreach (int index in playerLocations)
        {
            // Example: Assign a player to the selected landmass
            // Instantiate(playerPrefab, landmasses[index].transform.position, Quaternion.identity);
            Debug.Log("Player starting location: " + landmasses[index].transform.position);
        }
    }

    void Shuffle(List<int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
