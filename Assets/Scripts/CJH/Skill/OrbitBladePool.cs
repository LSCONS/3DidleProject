using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBladePool : MonoBehaviour
{
    public static OrbitBladePool Instance;

    public GameObject bladePrefab;
    public int initalPoolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < initalPoolSize; i++)
        {
            GameObject blade = Instantiate(bladePrefab);
            blade.SetActive(false);
            pool.Enqueue(blade);
        }
    }

    public GameObject GetBlade()
    {
        if (pool.Count > 0)
        {
            GameObject blade = pool.Dequeue();
            blade.SetActive(true);
            return blade;
        }
        else
        {
            GameObject newBlade = Instantiate(bladePrefab);
            return newBlade;
        }
    }

    public void ReturnBlade(GameObject blade)
    {
        blade.SetActive(false);
        pool.Enqueue(blade);
    }

}
