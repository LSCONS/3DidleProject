using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundTest : MonoBehaviour
{
    public Vector3 targetPos;
    public float duration;
    Vector3 lastPos;
    bool isExit;

    void Start()
    {
        StartCoroutine(Return());

    }

    IEnumerator Return()
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
