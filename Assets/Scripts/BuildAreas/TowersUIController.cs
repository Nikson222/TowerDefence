using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowersUIController : MonoBehaviour
{
    [SerializeField] private GameObject _buildPanel;
    [SerializeField] private GameObject _UpgradePanel;

    [SerializeField] private GameObject _towerItemParentPrefab;

    [SerializeField] private TowerStore _towerStore;
    [SerializeField] private List<BuildItem> _towerItems = new List<BuildItem>();

    public List<BuildItem> TowerItems => _towerItems;

    public bool _isClickOnUI;

    private void Awake()
    {
        foreach (var tower in _towerStore.TowerPrefabs)
        {
            var TowerItemsParent = Instantiate(_towerItemParentPrefab);
            TowerItemsParent.transform.SetParent(_buildPanel.transform);
            TowerItemsParent.transform.localScale = Vector3.one;

            var TowerItem = TowerItemsParent.GetComponentInChildren<BuildItem>();
            var toweSpriteRenderer = tower.GetComponent<SpriteRenderer>();
            TowerItem.InitItem(toweSpriteRenderer.sprite, tower, toweSpriteRenderer.color);

            _towerItems.Add(TowerItem);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                _isClickOnUI = true;
            else
                _isClickOnUI = false;
        }
    }


    public void EnablePanel(BuildArea buildArea)
    {
        DisablePanel();

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
