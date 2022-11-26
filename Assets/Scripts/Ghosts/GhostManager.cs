using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostManager : MonoBehaviour
{
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] Transform[] spawners;
    [SerializeField] Vector2[] ghostCounts;
    
    List<GameObject> ghosts = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            CreateGhosts(i, (int)ghostCounts[i].x, (int)ghostCounts[i].y);
        }
    }

    void CreateGhosts(int index, int min, int max)
    {
        for (int i = 0; i < Random.Range(min, max); i++)
        {
            CreateGhost(index);
        }
    }

    void CreateGhost(int index)
    {
        Transform temp = spawners[index];
        
        GameObject ghost = Instantiate(
            ghostPrefab,
            temp.position + (RandomFlatVector3() * Random.Range(5f, 30f)),
            Quaternion.identity,
            temp
        );
        
        ghosts.Add(ghost);
    }

    Vector3 RandomFlatVector3()
    {
        return new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f)
        );
    }
}