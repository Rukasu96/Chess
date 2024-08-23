using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastObjectHoveredSelection : MonoBehaviour, IHoveredSelector
{
    [SerializeField] private string selectableTag = "Selectable";
    private Transform hoveredObject;

    public bool IsObjectHovered(out ISelectable LocalSelectable)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.transform.CompareTag(selectableTag))
            {
                LocalSelectable = hit.transform.GetComponent<ISelectable>();
                hoveredObject = hit.transform;
                return true;
            }
        }
        LocalSelectable = null;
        hoveredObject = null;
        return false;
    }
    public Transform GetHoveredObject()
    {
        return hoveredObject;
    }
}
