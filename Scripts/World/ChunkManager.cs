using UnityEngine;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour
{
    public GameObject chunkPrefab;
    public IslandGenerator islandGenerator; // Reference to the island generator
    public int chunkSize = 100;
    public int viewDistance = 3;

    private Transform player;
    private Vector2Int currentChunkPosition;
    private Dictionary<Vector2Int, Chunk> loadedChunks = new Dictionary<Vector2Int, Chunk>();

    void Start()
    {
        player = Camera.main.transform;
        LoadChunksAroundPlayer();
    }

    void Update()
    {
        Vector2Int playerChunkPosition = WorldToChunkPosition(player.position);
        if (playerChunkPosition != currentChunkPosition)
        {
            UnloadChunks();
            LoadChunksAroundPlayer();
            currentChunkPosition = playerChunkPosition;
        }
    }

    void LoadChunksAroundPlayer()
    {
        // Load chunks based on islands
        foreach (var islandPosition in islandGenerator.LandmassPositions)
        {
            Vector2Int islandChunkPosition = WorldToChunkPosition(new Vector3(islandPosition.x, 0, islandPosition.y));
            for (int x = -viewDistance; x <= viewDistance; x++)
            {
                for (int y = -viewDistance; y <= viewDistance; y++)
                {
                    Vector2Int chunkPosition = islandChunkPosition + new Vector2Int(x, y);
                    if (!loadedChunks.ContainsKey(chunkPosition) && IsChunkWithinIsland(chunkPosition, islandPosition))
                    {
                        LoadChunk(chunkPosition);
                    }
                }
            }
        }
    }

    void LoadChunk(Vector2Int chunkPosition)
    {
        GameObject chunkObject = Instantiate(chunkPrefab, ChunkPositionToWorldPosition(chunkPosition), Quaternion.identity);
        Chunk chunk = chunkObject.GetComponent<Chunk>();
        chunk.coordinates = chunkPosition;
        chunk.LoadChunk();
        loadedChunks.Add(chunkPosition, chunk);
    }

    void UnloadChunks()
    {
        foreach (var chunk in loadedChunks.Values)
        {
            chunk.UnloadChunk();
            Destroy(chunk.gameObject);
        }
        loadedChunks.Clear();
    }

    Vector2Int WorldToChunkPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPosition.x / chunkSize), Mathf.FloorToInt(worldPosition.z / chunkSize));
    }

    Vector3 ChunkPositionToWorldPosition(Vector2Int chunkPosition)
    {
        return new Vector3(chunkPosition.x * chunkSize, 0, chunkPosition.y * chunkSize);
    }

    bool IsChunkWithinIsland(Vector2Int chunkPosition, Vector2 islandPosition)
    {
        Vector3 chunkCenter = ChunkPositionToWorldPosition(chunkPosition) + new Vector3(chunkSize / 2, 0, chunkSize / 2);
        float distanceToIslandCenter = Vector3.Distance(chunkCenter, new Vector3(islandPosition.x, 0, islandPosition.y));
        return distanceToIslandCenter <= islandGenerator.cellSize;
    }
}

