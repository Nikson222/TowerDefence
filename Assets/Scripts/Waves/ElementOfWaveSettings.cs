using UnityEngine;

[CreateAssetMenu(menuName = "Enemies Wave", order = 2, fileName = "New Enemy Wave")]
public class ElementOfWaveSettings : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _countOfEnemy;
    [SerializeField] private float _switchToNextElementDelay;

    public GameObject EnemyPrefab => _enemyPrefab; 
    public float SpawnDelay => _spawnDelay;
    public int CountOfEnemy => _countOfEnemy;
    public float SwitchToNextElementDelay => _switchToNextElementDelay;
}
