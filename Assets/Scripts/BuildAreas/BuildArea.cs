using System;
using UnityEngine;
using Towers;

[RequireComponent(typeof(Collider2D))]
public class BuildArea : MonoBehaviour
{
    public Action<BuildArea> OnClicked;
    public bool IsPlaced;
    public Tower TowerInArea;
    public bool IsClickedOnBuildArea;

    public bool MouseInArea;

    private void OnMouseUp()
    {
        OnClicked?.Invoke(this);    
    }
}
