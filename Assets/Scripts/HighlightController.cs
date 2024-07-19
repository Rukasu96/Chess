using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour, ISelectable
{
    //private Material highlightMaterial;
    private float scale;
    private float smoothSpeed = 0.25f;

    private void Start()
    {
        scale = transform.localScale.x;
       // highlightMaterial = GetComponent<Renderer>().materials[1];
    }
    /*
    private IEnumerator SetHighlightScale(float startValue, float endValue, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / duration;
           // highlightMaterial.SetFloat("_Scale", Mathf.Lerp(startValue, endValue, time));
            yield return new WaitForEndOfFrame();
        }
    }
    private void ChangeHighlightScale(float currentValue, float endValue)
    {
        StartCoroutine(SetHighlightScale(currentValue, endValue, smoothSpeed));
    }*/
    public void OnHover()
    {
        //ChangeHighlightScale(0f, 1.05f);
        transform.DOScale(scale*1.2f, 0.2f);
    }
    public void OnNotHover()
    {
        //ChangeHighlightScale(1.05f, 0f);
        transform.DOScale(scale, 0.5f);
    }
}
