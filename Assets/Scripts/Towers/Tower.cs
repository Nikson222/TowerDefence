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

        public int BuidPrice { get { return _buildPrice; } }
        public int UpgradePrice { get { return _updgadePrice; } }
        public int CurrentLevel { get { return _towerLevel; } }

        public abstract void UpgradeTower();
    }
}