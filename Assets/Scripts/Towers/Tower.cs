using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [Header("Prices")]
        [SerializeField] protected int _buildPrice;
        [SerializeField] protected int _updgadePrice;

        [Header("Level Settings")]
        [SerializeField] protected int _towerLevel = 1;

        [SerializeField] private TowerBlueprint UpgradeBlueprint;

        public int BuidPrice { get { return _buildPrice; } }
        public int UpgradePrice { get { return _updgadePrice; } }
        public int CurrentLevel { get { return _towerLevel; } }

        public virtual void UpgradeTower()
        {
            if (UpgradeBlueprint.Prefab != null)
            {
                BuildArea myBuildArea;
                transform.parent.TryGetComponent<BuildArea>(out myBuildArea);
                if (myBuildArea != null)
                {
                    TowerBuilder.Instance.BuildTower(myBuildArea, UpgradeBlueprint.Prefab);

                    Destroy(gameObject);
                }
            }
        }
    }
}