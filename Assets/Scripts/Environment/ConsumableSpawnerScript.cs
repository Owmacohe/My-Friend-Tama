using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawnerScript : MonoBehaviour  
{

    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> spawnableItems = new List<GameObject>();
    public int numberOfItemsToSpawn = 2;

    private List<Transform> freeSpawnPoint = new List<Transform>();
    private List<Transform> usedSpawnPoint = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        freeSpawnPoint.AddRange(spawnPoints);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < usedSpawnPoint.Count;)
        {
           if (usedSpawnPoint[i].childCount == 0)
            {
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
            //Stops while loop if there are no free spots left
            if (freeSpawnPoint.Count == 0)
            {
                break;
            }

            //Spawns new item
            var spawnPointIndex = Random.Range(0, freeSpawnPoint.Count);
            var itemToSpawnIndex = Random.Range(0, spawnableItems.Count);
            var spawnPoint = freeSpawnPoint[spawnPointIndex];
            var itemToSpawn = spawnableItems[itemToSpawnIndex];

            freeSpawnPoint.RemoveAt(spawnPointIndex);
            usedSpawnPoint.Add(spawnPoint);

            Instantiate(itemToSpawn, spawnPoint);

        }
    }
}
