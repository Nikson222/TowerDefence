using UnityEngine;
using UnityEngine.EventSystems;

public class TowersController : MonoBehaviour
{
    [SerializeField] private BuildArea[] _buildAreas;
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private TowerDisposer _towerDisposer;
    [SerializeField] private TowersUIController _UIManager;

    private bool _isClickOnBuildArea;
    public bool _isClickOnUI;
    private BuildArea _selectedBuildArea;

    #region Editor
#if UNITY_EDITOR
    [SerializeField] private GameObject _parentOfABuildAreas;
    private void OnValidate()
    {
        if (_parentOfABuildAreas != null)
            _buildAreas = _parentOfABuildAreas.GetComponentsInChildren<BuildArea>();
    }
#endif
    #endregion

    private void Awake()
    {
        //test getmoney -- will be deleted
        PlayerData.GetMoney(500);
    }

    private void Start()
    {
        _towerDisposer.OnTowerManaged += _UIManager.DisablePanel;
        _towerBuilder.OnBuild += _UIManager.DisablePanel;

        foreach (var area in _buildAreas)
        {
            area.OnClicked += SetSelectedBuildArea;
        }

        foreach (var TowerItem in _UIManager.TowerItems)
        {
            TowerItem.OnTowerSendedToBuilder += BuildTowerOnArea;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!_isClickOnBuildArea && !_UIManager._isClickOnUI)
            {
                _UIManager.DisablePanel();
            }
            _isClickOnBuildArea = false;
        }
    }

    private void BuildTowerOnArea()
    {
        _towerBuilder.BuildTower(_selectedBuildArea);
    }

    public void UpgradeTower()
    {
        _towerDisposer.UpgradeTower(_selectedBuildArea, _selectedBuildArea.TowerInArea.UpgradePrice);
    }

    public void SellTower()
    {
        _towerDisposer.SellTower(_selectedBuildArea, _selectedBuildArea.TowerInArea.BuidPrice);
    }

    private void SetSelectedBuildArea(BuildArea buildArea)
    {
        if (!_UIManager._isClickOnUI)
        {
            _isClickOnBuildArea = true;

            _selectedBuildArea = buildArea;
            _UIManager.EnablePanel(buildArea);
        }
    }
}
