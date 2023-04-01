using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDisposer : MonoBehaviour
{
    public Action OnTowerManaged;

    public void UpgradeTower(BuildArea selectedArea, int _towerUpgradeCost)
    {
        if (selectedArea.IsPlaced)
        {
            if (!PlayerData.CheckBalance(_towerUpgradeCost))
                return;
            selectedArea.TowerInArea.UpgradeTower();
            PlayerData.SpendMoney(_towerUpgradeCost);
            OnTowerManaged?.Invoke();
        }
    }

    public void SellTower(BuildArea selectedArea, int _towerBuildCost)
    {
        if (selectedArea.IsPlaced)
        {
            Destroy(selectedArea.TowerInArea.gameObject);
            selectedArea.IsPlaced = false;

            PlayerData.GetMoney(_towerBuildCost / 2);

            OnTowerManaged?.Invoke();
        }
    }
}
