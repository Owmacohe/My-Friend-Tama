using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawnerScript : MonoBehaviour  
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> spawnableItems;
    [SerializeField] int numberOfItemsToSpawn = 2;

    List<Transform> freeSpawnPoint = new List<Transform>();
    List<Transform> usedSpawnPoint = new List<Transform>();

    void Start()
    {
        freeSpawnPoint.AddRange(spawnPoints);
    }
    
    void Update()
    {
        for (int i = 0; i < usedSpawnPoint.Count;)
        {
           if (usedSpawnPoint[i].childCount == 0)
           {
                Debug.Log($"Remove item under {usedSpawnPoint[i].name}");
               freeSpawnPoint.Add(usedSpawnPoint[i]);
               usedSpawnPoint.RemoveAt(i);
           }
           else
           {
               i++;
           }
        }

        while (usedSpawnPoint.Count < numberOfItemsToSpawn)
        {
            // Stops while loop if there are no free spots left
            if (freeSpawnPoint.Count == 0)
            {
                break;
            }

            // Spawns new item
            int spawnPointIndex = Random.Range(0, freeSpawnPoint.Count);
            int itemToSpawnIndex = Random.Range(0, spawnableItems.Count);
            Transform spawnPoint = freeSpawnPoint[spawnPointIndex];
            GameObject itemToSpawn = spawnableItems[itemToSpawnIndex];

            freeSpawnPoint.RemoveAt(spawnPointIndex);
            usedSpawnPoint.Add(spawnPoint);

            Instantiate(itemToSpawn, spawnPoint);

            Debug.Log($"Spawn item under {spawnPoint.name}");
        }
    }
}
