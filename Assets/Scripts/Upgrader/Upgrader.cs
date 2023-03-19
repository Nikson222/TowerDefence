using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrader : MonoBehaviour
{
    public Action OnUpgrade;

    public void UpgradeTower(BuildArea selectedArea)
    {
        if (selectedArea.IsPlaced)
        {
            selectedArea.TowerInArea.UpgradeTower();
            OnUpgrade?.Invoke();
        }
    }
}
