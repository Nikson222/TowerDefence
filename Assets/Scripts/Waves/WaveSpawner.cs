using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parentForSpawned;

    [SerializeField] private WayPoint[] _waypoints;

    [SerializeField] private Wave[] _waves;
    private int _waveIndex = 0;

    [SerializeField] EnemiesPooler _enemyPooler;

    private Action OnWaveSpawned;

    #region Editor
#if UNITY_EDITOR
    [SerializeField] private GameObject _parentOfWaypoints;
    private void OnValidate()
    {
        if (_parentOfWaypoints != null && _waypoints.Length == 0)
            _waypoints = _parentOfWaypoints.GetComponentsInChildren<WayPoint>();
    }
#endif
    #endregion

    private void Start()
    {
        _waveIndex = 0;

        _enemyPooler.Init(WeedOutRepeatingEnemies());

        OnWaveSpawned += StartWave;

        StartWave();
    }

    private void StartWave()
    {
        StartCoroutine(StartWaveCoroutine());
    }

    private IEnumerator StartWaveCoroutine()
    {
        yield return new WaitForSeconds(_waves[_waveIndex].StartWaveDelay);
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int _elementIndex = 0; _elementIndex < _waves[_waveIndex].ElemensOfWaveSettings.Length; _elementIndex++)
        {
            for (int enemyCount = _waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].CountOfEnemy; enemyCount > 0; enemyCount--)
            {
                var spawnedEnemy = _enemyPooler.GiveEnemies(_waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].EnemyPrefab);
                spawnedEnemy.InitPath(_waypoints);
                spawnedEnemy.gameObject.transform.position = _waves[_waveIndex].SpawnPosition.position;

                yield return new WaitForSeconds(_waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].SpawnDelay);
            }

            yield return new WaitForSeconds(_waves[_waveIndex].ElemensOfWaveSettings[_elementIndex].SwitchToNextElementDelay);
        }

        _waveIndex++;
        if(_waveIndex < _waves.Length && OnWaveSpawned != null)
            OnWaveSpawned.Invoke();
    }

    private HashSet<MoveableEnemy> WeedOutRepeatingEnemies()
    {
        HashSet<MoveableEnemy> result = new HashSet<MoveableEnemy>();

        foreach (var wave in _waves)
        {
            foreach (var element in wave.ElemensOfWaveSettings)
            {
                result.Add(element.EnemyPrefab.GetComponent<MoveableEnemy>());
            }
        }

        return result;
    }
}
