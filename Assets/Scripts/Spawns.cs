using UnityEngine;

public class Spawns : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;
        public float spawnRate;
        public int count;
    }

    public EnemyType[] enemies;
    public Camera gameCamera;
    private float[] nextSpawnTime;

    void Start()
    {
        nextSpawnTime = new float[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            nextSpawnTime[i] = enemies[i].spawnRate;
        }
    }

    void Update()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Time.time >= nextSpawnTime[i])
            {
                SpawnEnemy(i);
                enemies[i].count++;
                // Ajusta la frecuencia de generación aumentando la dificultad.
                nextSpawnTime[i] = Time.time + enemies[i].spawnRate / (1 + 0.1f * enemies[i].count);
            }
        }
    }

    void SpawnEnemy(int index)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(enemies[index].prefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
{
    float x, y;
    Vector3 spawnPosition = Vector3.zero;

    // Determina de qué borde aparecerá el enemigo
    int edge = Random.Range(0, 4);  // 0 = izquierda, 1 = derecha, 2 = abajo, 3 = arriba

    switch (edge)
    {
        case 0: // Izquierda
            x = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, gameCamera.nearClipPlane)).x;
            y = Random.Range(gameCamera.ViewportToWorldPoint(new Vector3(0, 0, gameCamera.nearClipPlane)).y,
                             gameCamera.ViewportToWorldPoint(new Vector3(0, 1, gameCamera.nearClipPlane)).y);
            spawnPosition = new Vector3(x, y, 0);
            break;
        case 1: // Derecha
            x = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, gameCamera.nearClipPlane)).x;
            y = Random.Range(gameCamera.ViewportToWorldPoint(new Vector3(0, 0, gameCamera.nearClipPlane)).y,
                             gameCamera.ViewportToWorldPoint(new Vector3(0, 1, gameCamera.nearClipPlane)).y);
            spawnPosition = new Vector3(x, y, 0);
            break;
        case 2: // Abajo
            y = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, gameCamera.nearClipPlane)).y;
            x = Random.Range(gameCamera.ViewportToWorldPoint(new Vector3(0, 0, gameCamera.nearClipPlane)).x,
                             gameCamera.ViewportToWorldPoint(new Vector3(1, 0, gameCamera.nearClipPlane)).x);
            spawnPosition = new Vector3(x, y, 0);
            break;
        case 3: // Arriba
            y = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, gameCamera.nearClipPlane)).y;
            x = Random.Range(gameCamera.ViewportToWorldPoint(new Vector3(0, 0, gameCamera.nearClipPlane)).x,
                             gameCamera.ViewportToWorldPoint(new Vector3(1, 0, gameCamera.nearClipPlane)).x);
            spawnPosition = new Vector3(x, y, 0);
            break;
    }

    return spawnPosition;
}

}
