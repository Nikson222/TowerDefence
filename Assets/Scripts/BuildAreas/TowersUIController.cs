using System.Collections.Generic;
using UnityEngine;

public class TowersUIController : MonoBehaviour
{
    [SerializeField] private TowerStore _towerStore;
    [SerializeField] private GameObject _buildPanel;
    [SerializeField] private GameObject _towerItemParentPrefab;
    [SerializeField] private List<TowerItem> _towerItems = new List<TowerItem>();
    [SerializeField] private GameObject _UpgradePanel;
    public List<TowerItem> TowerItems => _towerItems;

    private void Awake()
    {
        foreach (var tower in _towerStore.TowerPrefabs)
        {
            var TowerItemsParent = Instantiate(_towerItemParentPrefab);
            TowerItemsParent.transform.SetParent(_buildPanel.transform);
            TowerItemsParent.transform.localScale = Vector3.one;

            var TowerItem = TowerItemsParent.GetComponentInChildren<TowerItem>();
            var toweSpriteRenderer = tower.GetComponent<SpriteRenderer>();
            TowerItem.InitItem(toweSpriteRenderer.sprite, tower, toweSpriteRenderer.color);

            _towerItems.Add(TowerItem);
        }
    }

    public void EnablePanel(BuildArea buildArea)
    {
        if (_buildPanel != null && _UpgradePanel != null)
        {
            if (!buildArea.IsPlaced)
                _buildPanel.SetActive(true);
            else
                _UpgradePanel.SetActive(true);
        }
    }

    public void DisablePanel()
    {
        _buildPanel.SetActive(false);
        _UpgradePanel.SetActive(false);
    }
}
