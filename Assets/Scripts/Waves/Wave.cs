using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private float _startWaveDelay;
    [SerializeField] Transform _spawnPosition;
    [SerializeField] private ElementOfWaveSettings[] _elemensOfWaveSettings;
    public ElementOfWaveSettings[] ElemensOfWaveSettings => _elemensOfWaveSettings;
    public Transform spawnPosition => _spawnPosition;
    public float StartWaveDelay => _startWaveDelay;
}
