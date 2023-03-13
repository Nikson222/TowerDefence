public interface ITower
{
    public int BuidCost { get;}
    public int UpgradeCost { get;}
    public int CurrentLevel { get; set; }
    public int MaxAvailableLevel { get;}
    public void UpgradeTower();
}
