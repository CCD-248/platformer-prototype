using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    private Queue<GameObject> afterImageQueue = new Queue<GameObject>();

    public int countOfAfterImages = 10;
    public static PlayerAfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        //GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < countOfAfterImages; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        afterImageQueue.Enqueue(obj);
    }

    public GameObject GetFromPool()
    {
        if (afterImageQueue.Count == 0)
        {
            GrowPool();
        }
        var instance = afterImageQueue.Dequeue();   
        instance.SetActive(true);
        return instance;
    }

}
