using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject[] itemPrefabs; 
    public Transform[] spawnPoints; 
    public float spawnTime = 30f; 

    private void Start()
    {
        StartCoroutine(SpawnItemRoutine());
    }

    private IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            foreach (var point in spawnPoints)
            {
                if (point.childCount == 0) 
                {
                    SpawnItem(point);
                }
            }
        }
    }

    public void itemAlMorir(Vector3 position) 
    {
        
         if (Random.Range(0, 100) < 5)  // Probabilidad del 5%
        {
            int itemIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[itemIndex], position, Quaternion.identity);
        }
    }

    void SpawnItem(Transform spawnPoint)
    {
        int itemIndex = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[itemIndex], spawnPoint.position, Quaternion.identity, spawnPoint);
    }
}
