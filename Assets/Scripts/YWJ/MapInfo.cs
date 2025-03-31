using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public MapController controller;
    public LayerMask playerLayer;
    public Transform[] spawners;

    public void Init()
    {
        controller = FindObjectOfType<MapController>();
        controller.spawners.Add(spawners[0]);
        controller.spawners.Add(spawners[1]);
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
