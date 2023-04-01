using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BuildItem : MonoBehaviour
{
    public Action OnTowerSendedToBuilder;
    private Image _buttonImage;
    private GameObject _towerObjectToSpawn;
    [SerializeField] private Text _towerCostText;
    [SerializeField] private int _towerCost;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void InitItem(Sprite image, GameObject towerPrefab, Color spriteColor, int towerBuildCots)
    {
        _buttonImage.sprite = image;
        _towerObjectToSpawn = towerPrefab;
        _buttonImage.color = spriteColor;
        _towerCost = towerBuildCots;
        _towerCostText.text = _towerCost.ToString();
    }

    public void SendPrefabToBuilder()
    {
        TowerBuilder.Instance.GetBuildInfo(_towerObjectToSpawn, _towerCost);
        OnTowerSendedToBuilder();
    }
}
