using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> Enemies;

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

        foreach (Enemy e in Enemies)
        {
            e.gameObject.SetActive(true);
            e.Reset();
        }
    }
}
