using System;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField] private float _startWaveDelay;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private ElementOfWaveSettings[] _elemensOfWaveSettings;
    public ElementOfWaveSettings[] ElemensOfWaveSettings => _elemensOfWaveSettings;
    public Transform SpawnPosition => _spawnPosition;
    public float StartWaveDelay => _startWaveDelay;
}
