using UnityEngine;

public class TurretTower : ShootingTower, ITower
{
    public int BuidCost { get => _buildPrice; }
    public int UpgradeCost { get => _updgadePrice; }
    public int CurrentLevel { get => _towerLevel; set => _towerLevel = value; }
    public int MaxAvailableLevel { get => _updgadePrice; }

    public void UpgradeTower()
    {
        if (!(CurrentLevel + 1 > MaxAvailableLevel))
        {
            CurrentLevel++;
            foreach (var upgrade in upgrades)
            {
                if (upgrade.level == CurrentLevel)
                {
                    _updgadePrice = upgrade.newUpgradeCost;
                    _damage = upgrade.newDamage;
                    _attackRadius = upgrade.newRange;
                    SetMovementStrategy(upgrade.ProjectileStrategy);

                    if (upgrade.newSprite != null)
                        GetComponent<SpriteRenderer>().sprite = upgrade.newSprite;

                    break;
                }
            }
        }
    }
}


