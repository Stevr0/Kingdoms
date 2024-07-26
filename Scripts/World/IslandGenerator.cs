using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
	public int gridWidth = 10; // Width of the grid
	public int gridHeight = 10; // Height of the grid
	public int landmassCount = 50; // Number of landmasses
	public float cellSize = 100f; // Size of each grid cell
	public GameObject landmassPrefab; // Prefab for landmasses
	public GameObject waterPrefab; // Prefab for water

	private List<Vector2> gridPositions = new List<Vector2>();
	private List<Vector2> availableStartingLocations = new List<Vector2>();
	private List<Vector2> playerStartingLocations = new List<Vector2>();

	// Store landmass positions
	public List<Vector2> LandmassPositions { get; private set; } = new List<Vector2>();

	void Start()
	{
		float worldWidth = gridWidth * cellSize;
		float worldHeight = gridHeight * cellSize;

		Debug.Log("World Width: " + worldWidth + " units");
		Debug.Log("World Height: " + worldHeight + " units");

		GenerateGridPositions();
		Shuffle(gridPositions);
		PlaceLandmasses();
		PlaceWater();
		DefineStartingLocations();
	}

	void GenerateGridPositions()
	{
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				gridPositions.Add(new Vector2(x * cellSize, y * cellSize));
			}
		}
	}

	void Shuffle(List<Vector2> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			Vector2 value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	void PlaceLandmasses()
	{
		for (int i = 0; i < landmassCount; i++)
		{
			Vector2 position = gridPositions[i];
			Instantiate(landmassPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
			availableStartingLocations.Add(position);
			LandmassPositions.Add(position); // Store landmass positions
		}
	}

	void PlaceWater()
	{
		HashSet<Vector2> landmassPositions = new HashSet<Vector2>();
		for (int i = 0; i < landmassCount; i++)
		{
			landmassPositions.Add(gridPositions[i]);
		}

		foreach (Vector2 position in gridPositions)
		{
			if (!landmassPositions.Contains(position))
			{
				Instantiate(waterPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
			}
		}
	}

	void DefineStartingLocations()
	{
		// Ensure there is at least one empty landmass between players
		List<Vector2> potentialStartingLocations = new List<Vector2>(availableStartingLocations);
		Shuffle(potentialStartingLocations);

		while (playerStartingLocations.Count < 50 && potentialStartingLocations.Count > 0)
		{
			Vector2 candidate = potentialStartingLocations[0];
			potentialStartingLocations.RemoveAt(0);

			bool valid = true;
			foreach (Vector2 location in playerStartingLocations)
			{
				if (Vector2.Distance(candidate, location) < cellSize * 2)
				{
					valid = false;
					break;
				}
			}

			if (valid)
			{
				playerStartingLocations.Add(candidate);
			}
		}

		// Debug: Print starting locations
		foreach (Vector2 loc in playerStartingLocations)
		{
			Debug.Log("Starting Location: " + loc);
		}
	}
}

