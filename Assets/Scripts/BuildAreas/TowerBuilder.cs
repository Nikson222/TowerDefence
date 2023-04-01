using System;
using UnityEngine;
using Towers;

public class TowerBuilder : MonoBehaviour
{
    public Action OnBuild;

    static public TowerBuilder Instance;
    private GameObject _towerPrefab;
    private int _towerCost;

    private void Start()
    {
        Instance = this;
    }

    public void GetBuildInfo(GameObject towerPrefab, int towerCost)
    {
        _towerPrefab = towerPrefab;
        _towerCost = towerCost;
    }

    public void BuildTower(BuildArea buildArea)
    {

        if (buildArea != null)
        {
            if (!buildArea.IsPlaced)
            {
                if (!PlayerData.CheckBalance(_towerCost))
                    return;
                var tower = Instantiate(_towerPrefab, buildArea.transform.position, Quaternion.identity);
                tower.transform.SetParent(buildArea.transform);
                buildArea.TowerInArea = tower.GetComponent<Tower>();
                buildArea.IsPlaced = true;
                PlayerData.SpendMoney(_towerCost);
            }
        }
        OnBuild?.Invoke();
    }

    public void BuildTower(BuildArea buildArea, GameObject prefab)
    {
        if (buildArea != null && prefab != null)
        {
            if (!PlayerData.CheckBalance(_towerCost))
                return;
            var tower = Instantiate(prefab, buildArea.transform.position, Quaternion.identity);
            tower.transform.SetParent(buildArea.transform);
            buildArea.TowerInArea = tower.GetComponent<Tower>();
            buildArea.IsPlaced = true;
            PlayerData.SpendMoney(_towerCost);
        }
        OnBuild?.Invoke();
    }
}
