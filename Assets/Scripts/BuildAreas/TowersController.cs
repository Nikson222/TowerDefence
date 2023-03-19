using UnityEngine;
using UnityEngine.EventSystems;

public class TowersController : MonoBehaviour
{
    [SerializeField] private BuildArea[] _buildAreas;
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private Upgrader _towerUpgrader;
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

    private void Start()
    {
        _towerUpgrader.OnUpgrade += _UIManager.DisablePanel;
        _towerBuilder.OnBuild += _UIManager.DisablePanel;

        foreach (var area in _buildAreas)
        {
            area.OnClicked += SetSelectedBuildArea;
        }

        foreach (var TowerItem in _UIManager.TowerItems)
        {
            TowerItem.OnTowerSendedToBuilder += SpawnTowerOnArea;
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

    private void SpawnTowerOnArea()
    {
        _towerBuilder.BuildTower(_selectedBuildArea);
    }

    public void UpgradeTower()
    {
        _towerUpgrader.UpgradeTower(_selectedBuildArea);
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
