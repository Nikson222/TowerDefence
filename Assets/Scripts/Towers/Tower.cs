using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [Header("Prices")]
        [SerializeField] protected int _buildPrice;
        [SerializeField] protected int _updgadePrice;

        [SerializeField] private TowerBlueprint UpgradeBlueprint;

        public bool IsCanBeUpgrade;
        
        protected virtual void Start()
        {
            IsCanBeUpgrade = UpgradeBlueprint.Prefab != null;
        }

        public int BuidPrice { get { return _buildPrice; } }
        public int UpgradePrice { get { return _updgadePrice; } }

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