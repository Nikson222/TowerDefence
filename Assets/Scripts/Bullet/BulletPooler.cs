using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler Instance;

    [SerializeField] private int _poolSize;
    [SerializeField] private int _insufficientPoolSize;

    
    private Dictionary<ShootingTower, Queue<Bullet>> _poolerDictionary = new Dictionary<ShootingTower, Queue<Bullet>>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SpawnPool(Bullet bulletPrefab, ShootingTower tower)
    {
        _poolerDictionary.Add(tower, new Queue<Bullet>());

        for (int i = 0; i < _poolSize; i++)
        {
            Bullet spawnedBullet = Instantiate(bulletPrefab);
            spawnedBullet.gameObject.SetActive(false);

            _poolerDictionary[tower].Enqueue(spawnedBullet);
        }
    }

    public Bullet GiveBullet(ShootingTower tower)
    {
        for (int i = 0; i < _poolerDictionary[tower].Count; i++)
        {
            var bullet = _poolerDictionary[tower].Dequeue();
            _poolerDictionary[tower].Enqueue(bullet);

            if (bullet.gameObject.activeInHierarchy.Equals(true))
            {
                continue;
            }
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        Bullet newBullet = SpawnInsufficientEnemies(tower);
        newBullet.gameObject.SetActive(true);
        return newBullet;

    }

    private Bullet SpawnInsufficientEnemies(ShootingTower tower)
    {
        Bullet spawnedBullet = null;

        for (int i = 0; i < _insufficientPoolSize; i++)
        {
            spawnedBullet = Instantiate(_poolerDictionary[tower].Peek().gameObject).GetComponent<Bullet>();
            spawnedBullet.gameObject.SetActive(false);

            _poolerDictionary[tower].Enqueue(spawnedBullet);
        }

        return spawnedBullet;
    }
}
