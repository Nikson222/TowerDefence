using UnityEngine;

public class BuildAreasController : MonoBehaviour
{
    [SerializeField] private BuildArea[] _buildAreas;
    [SerializeField] private TowersUIController _UIManager;
    [SerializeField] private TowerBuilder _towerBuilder;
    private bool _isClickOnBuildArea;
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
            if (!_isClickOnBuildArea)
                _UIManager.DisablePanel();
            else
                _isClickOnBuildArea = false;
        }
    }

    private void SpawnTowerOnArea()
    {
        _towerBuilder.BuildTower(_selectedBuildArea);
    }

    private void SetSelectedBuildArea(BuildArea buildArea)
    {
        UpdateClickState(true);

        _selectedBuildArea = buildArea;
        _UIManager.EnablePanel(buildArea);
    }

    public void UpdateClickState(bool clickOnBuildArea)
    {
        _isClickOnBuildArea = clickOnBuildArea;
    }

}
