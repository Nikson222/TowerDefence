using System;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public Action OnBuild;

    static public TowerBuilder Instance;
    private GameObject _towerPrefab;

    private void Start()
    {
        Instance = this;
    }

    public void GetPrefabToSpawn(GameObject towerPrefab)
    {
        _towerPrefab = towerPrefab;
    }

    public void BuildTower(BuildArea buildArea)
    {
        if (buildArea != null)
        {
            if (!buildArea.IsPlaced)
            {
                var tower = Instantiate(_towerPrefab, buildArea.transform.position, Quaternion.identity);
                tower.transform.SetParent(buildArea.transform);
                buildArea.TowerInArea = tower.GetComponent<ITower>();
                buildArea.IsPlaced = true;
            }
        }
        OnBuild?.Invoke();
    }

    private void OnMouseDown()
    {
        print("dd");
    }
}
