using DG.Tweening;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private bool rotateFinished;

    public void RotateBoard()
    {
        rotateFinished = false;
        float YRotationValue = transform.eulerAngles.y + 180;
        transform.DORotate(new Vector3(0f,YRotationValue,0f), 1f).OnComplete(() => rotateFinished = true);
    }

    public bool IsRotationFinish()
    {
        return rotateFinished;
    }
}
