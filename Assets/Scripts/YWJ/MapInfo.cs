using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public MapController controller;
    public LayerMask playerLayer;

    private void Awake()
    {
        controller = FindObjectOfType<MapController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ãæµ¹");
        if ((playerLayer & (1 << collision.gameObject.layer)) != 0)
        {
            controller.playerMapPosition = this.transform.position;
            controller.ChangeMap?.Invoke();
        }
    }
}
