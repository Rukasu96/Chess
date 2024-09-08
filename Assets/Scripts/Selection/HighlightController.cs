using DG.Tweening;
using UnityEngine;

public class HighlightController : MonoBehaviour, ISelectable
{
    private float startingScale;
    private float scaleTime = 0.2f;
    private bool isSelected = false;

    private void Start()
    {
        startingScale = transform.localScale.x;
    }

    public void OnHover()
    {
        transform.DOScale(startingScale*1.2f, scaleTime);
    }

    public void OnNotHover()
    {
        transform.DOScale(startingScale, 0.5f);
    }

    public void SelectionUpdate()
    {
        isSelected = !isSelected;
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
