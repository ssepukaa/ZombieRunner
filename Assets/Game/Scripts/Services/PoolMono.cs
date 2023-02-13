using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }

    public Transform Container { get; }
    private List<T> pool;


    public PoolMono(T prefab, int count)
    {
        this.Prefab = prefab;
        this.Container = null;
        this.CreatePool(count);

    }

    public PoolMono(T prefab, int count, Transform container)
    {
        this.Prefab=prefab;

        this.Container=container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool=  new List<T>();
        for (int i = 0; i<count; i++)
        {
            this.CreateObject();
        }

    }
    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.Prefab, this.Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }
    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }

        }
        element = null;
        return false;
    }
    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
        {
            
            return element;
        }
        if (this.AutoExpand)
        {
            return this.CreateObject(true);
        }
        throw new System.Exception($"There is no free element in pool of type {typeof(T)}");


    }

}
