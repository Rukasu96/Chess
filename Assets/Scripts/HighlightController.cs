using DG.Tweening;
using UnityEngine;

public class HighlightController : MonoBehaviour, ISelectable
{
    private float scale;
    private float scaleTime = 0.2f;
    private bool isSelected = false;

    private void Start()
    {
        scale = transform.localScale.x;
    }

    public void OnHover()
    {
        transform.DOScale(scale*1.2f, scaleTime);
    }

    public void OnNotHover()
    {
        transform.DOScale(scale, 0.5f);
    }

    public void Selected()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
