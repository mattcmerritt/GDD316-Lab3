using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> Enemies;
    [SerializeField] private GameObject EnemyPrefab;

    public void Update()
    {
        bool oneAlive = false;
        foreach (Enemy e in Enemies)
        {
            if (e.GetIsAlive())
            {
                oneAlive = true;
            }
        }

        if (!oneAlive)
        {
            StartCoroutine(RespawnEnemies());
        }
    }

    private IEnumerator RespawnEnemies()
    {
        yield return new WaitForSeconds(5f);

        List<Enemy> newEnemies = new List<Enemy>();
        foreach (Enemy e in Enemies)
        {
            Vector3 start = e.GetStartLocation();
            Instantiate(EnemyPrefab, start, Quaternion.identity);
        }

        Enemies = newEnemies;
    }
}
