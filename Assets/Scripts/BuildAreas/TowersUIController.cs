using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Towers;

public class TowersUIController : MonoBehaviour
{
    [SerializeField] private GameObject _buildPanel;
    [SerializeField] private GameObject _managementPanel;

    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Text _sellButtonText;
    [SerializeField] private Text _upgradeButtonText;

    [SerializeField] private GameObject _towerItemParentPrefab;

    [SerializeField] private TowerStore _towerStore;
    [SerializeField] private List<BuildItem> _towerItems = new List<BuildItem>();

    [SerializeField] public PlayerMoneyDisplayer _displayerPlayerMoney;

    [SerializeField] private BuildArea _currentSelectedBuildArea;

    public List<BuildItem> TowerItems => _towerItems;

    public bool _isClickOnUI;

    private void Awake()
    {
        PlayerData.OnMoneyChanged += _displayerPlayerMoney.UpdateText;
        PlayerData.OnMoneyChanged += UpdateButtonsState;

        foreach (var tower in _towerStore.TowerPrefabs)
        {
            var TowerItemsParent = Instantiate(_towerItemParentPrefab);
            TowerItemsParent.transform.SetParent(_buildPanel.transform);
            TowerItemsParent.transform.localScale = Vector3.one;

            var TowerItem = TowerItemsParent.GetComponentInChildren<BuildItem>();
            var toweSpriteRenderer = tower.GetComponent<SpriteRenderer>();
            TowerItem.InitItem(toweSpriteRenderer.sprite, tower, toweSpriteRenderer.color, tower.GetComponent<Tower>().BuidPrice);

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
        _currentSelectedBuildArea = buildArea;

        if (_buildPanel != null && _managementPanel != null)
        {
            if (!buildArea.IsPlaced)
                _buildPanel.SetActive(true);
            else
            {
                _managementPanel.SetActive(true);

                UpdateButtonsState();

                _upgradeButtonText.text = buildArea.TowerInArea.UpgradePrice.ToString();
                _sellButtonText.text = (buildArea.TowerInArea.BuidPrice / 2).ToString();
            }
        }
    }

    private void UpdateButtonsState()
    {
        if (_currentSelectedBuildArea != null && _currentSelectedBuildArea.IsPlaced)
        {
            if (!_currentSelectedBuildArea.TowerInArea.IsCanBeUpgrade)
                _upgradeButton.gameObject.SetActive(false);
            else
            {
                _upgradeButton.gameObject.SetActive(true);

                _upgradeButton.interactable = PlayerData.CheckBalance(_currentSelectedBuildArea.TowerInArea.UpgradePrice);
            }
        }
    }

    public void DisablePanel()
    {
        _currentSelectedBuildArea = null;
        _buildPanel.SetActive(false);
        _managementPanel.SetActive(false);
    }
}
