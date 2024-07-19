using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoveredSelector
{
    bool IsObjectHovered(Ray ray, out ISelectable LocalSelectable);
    Transform GetHoveredObject();
}
