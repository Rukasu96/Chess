using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer renderer;
    private Color defaultColor;
    public Chessman Chessman;
    public PositionOnGrid PositionOnGrid;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    public void SetColor(Color color)
    {
        defaultColor = color;
        renderer.material.color = defaultColor;
    }
    public void ChangeColor(Color color)
    {
        renderer.material.color = color;
    }
    public void BackToDefaultColor()
    {
        renderer.material.color = defaultColor;
    }
}
