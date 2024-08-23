using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour, ISelectable
{
    private float scale;
    private float scaleTime = 0.2f;

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
}
