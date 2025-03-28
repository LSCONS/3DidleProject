using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum Type
{
    Sine,
    Cubic
}

public class BackgroundTest : MonoBehaviour
{
    public Type type;
    public Vector3 targetPos;
    public float duration;

    Vector3 lastPos;
    bool isExit;

    void Start()
    {
        switch (type)
        {
            case Type.Sine:
                StartCoroutine(Sine());
                break;
            case Type.Cubic:
                StartCoroutine(Cubic());
                break;
        }
    }

    IEnumerator Sine()
    {
        while (true)
        {
            lastPos = transform.position;
            transform.DOMove(transform.position + targetPos, duration).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(duration);
            transform.DOMove(lastPos, duration).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(duration);

            if (isExit) break;
        }
        yield return null;
    }

    IEnumerator Cubic()
    {
        while (true)
        {
            lastPos = transform.position;
            transform.DOMove(transform.position + targetPos, duration).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(duration);
            transform.DOMove(lastPos, duration).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(duration);

            if (isExit) break;
        }
        yield return null;
    }
}
