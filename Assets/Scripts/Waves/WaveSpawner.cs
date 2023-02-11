using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parentForSpawned;

    [SerializeField] private GameObject _parentOfWaypoints;
    private WayPoint[] waypoints;

    [SerializeField] private Wave[] _waves;
    private int _waveIndex = 0;
    private int _elementIndex = 0;
    private int _elementsleftToSpawn;
    private int _currentElementEnemyiesleftToSpawn;

    //private Action<int> OnElementSpawnCompleted;
    //private Coroutine spawnCoroutine;
    private void Start()
    {
        waypoints = _parentOfWaypoints.GetComponentsInChildren<WayPoint>();
        _elementsleftToSpawn = _waves[_waveIndex].ElemensOfWaveSettings.Length;
        _currentElementEnemyiesleftToSpawn = _waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].countOfEnemy;

        StartCoroutine(SpawnEnemiesFromElementOfWave());
    }

    //private void LaunchWave(int waveIdex)
    //{
    //    spawnCoroutine = StartCoroutine(SpawnEnemiesFromElementOfWave());
    //}

    private IEnumerator SpawnEnemiesFromElementOfWave()
    {
        yield return new WaitForSeconds(_waves[_waveIndex].StartWaveDelay);

        while (true)
        {
            if (_currentElementEnemyiesleftToSpawn > 0)
            {
                var spawnedEnemy = Instantiate(_waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].EnemyPrefab, _parentForSpawned).GetComponent<MoveableEnemy>();
                spawnedEnemy.InitPath(waypoints);
                spawnedEnemy.gameObject.transform.position = _waves[_waveIndex].spawnPosition.position;

                _currentElementEnemyiesleftToSpawn--;
                yield return new WaitForSeconds(_waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].spawnDelay);
            }
            else
            {
                _elementIndex++;
                _elementsleftToSpawn--;

                if (_elementIndex < _waves[_waveIndex].ElemensOfWaveSettings.Length)
                {
                    yield return new WaitForSeconds(_waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].SwitchToNextElementDelay);
                    SetNeededCountenemiesToSpawn();
                }
                else
                {
                    _waveIndex++;
                    if (_waveIndex < _waves.Length)
                    {
                        InitWave();
                        yield return new WaitForSeconds(_waves[_waveIndex].StartWaveDelay);
                    }
                    else
                        break;
                }
            }
        }
    }

    private void InitWave()
    {
        _elementIndex = 0;

        _elementsleftToSpawn = _waves[_waveIndex].ElemensOfWaveSettings.Length;
        _currentElementEnemyiesleftToSpawn = _waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].countOfEnemy;

    }

    private void SetNeededCountenemiesToSpawn()
    {
        _currentElementEnemyiesleftToSpawn = _waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].countOfEnemy;
    }
}
