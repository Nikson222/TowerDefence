using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemiesPooler : MonoBehaviour
{
    [SerializeField] private int _poolSize;
    [SerializeField] private int _insufficientPoolSize;

    Dictionary<GameObject, Queue<MoveableEnemy>> _poolerDictionary;

    public void Init(HashSet<MoveableEnemy> enemiesTypes)
    {
        _poolerDictionary = new Dictionary<GameObject, Queue<MoveableEnemy>>();
        SpawnPool(enemiesTypes);
    }

    private void SpawnPool(HashSet<MoveableEnemy> enemiesTypes)
    {
        foreach (var enemy in enemiesTypes)
        {
            _poolerDictionary.Add(enemy.gameObject, new Queue<MoveableEnemy>());

            for (int i = 0; i < _poolSize; i++)
            {
                MoveableEnemy spawnedEnemy = Instantiate(enemy.gameObject, transform).GetComponent<MoveableEnemy>();
                spawnedEnemy.gameObject.SetActive(false);

                _poolerDictionary[enemy.gameObject].Enqueue(spawnedEnemy);
            }
        }
    }

    public MoveableEnemy GiveEnemies(GameObject enemyPrefab)
    {
        for (int i = 0; i < _poolerDictionary[enemyPrefab].Count; i++)
        {
            var enemy = _poolerDictionary[enemyPrefab].Dequeue();
            _poolerDictionary[enemyPrefab].Enqueue(enemy);

            if (enemy.gameObject.activeInHierarchy.Equals(true))
            {
                continue;
            }
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        MoveableEnemy newEnemy = SpawnInsufficientEnemies(enemyPrefab);
        newEnemy.gameObject.SetActive(true);
        return newEnemy;

    }

    private MoveableEnemy SpawnInsufficientEnemies(GameObject enemyPrefab)
    {
        MoveableEnemy spawnedEnemy = null;

        for (int i = 0; i < _insufficientPoolSize; i++)
        {
            spawnedEnemy = Instantiate(enemyPrefab, transform).GetComponent<MoveableEnemy>();
            spawnedEnemy.gameObject.SetActive(false);

            _poolerDictionary[enemyPrefab].Enqueue(spawnedEnemy);
        }

        return spawnedEnemy;
    }
}