using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOneSide : MonoBehaviour
{
    public Type type;
    public Vector3 targetPos;
    public float duration;

    Vector3 lastPos;
    bool isShow;

    void Start()
    {
        lastPos = transform.position;
        targetPos += transform.position;
    }

    public void OnClickSetting()
    {
        if (!isShow)
        {
            switch (type)
            {
                case Type.Default:
                    transform.DOMove(targetPos, duration);
                    break;
                case Type.Sine:
                    transform.DOMove(targetPos, duration).SetEase(Ease.InOutSine);
                    break;
                case Type.Cubic:
                    transform.DOMove(targetPos, duration).SetEase(Ease.InOutCubic);
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case Type.Default:
                    transform.DOMove(lastPos, duration);
                    break;
                case Type.Sine:
                    transform.DOMove(lastPos, duration).SetEase(Ease.InOutSine);
                    break;
                case Type.Cubic:
                    transform.DOMove(lastPos, duration).SetEase(Ease.InOutCubic);
                    break;
            }
        }

        isShow = !isShow;
    }
}
