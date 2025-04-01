using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
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
        Debug.Log("충돌");
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.playerMapPosition = this.transform.position;
            controller.ChangeMap?.Invoke();
            controller.meshSurface.BuildNavMesh();
        }
    }
}
