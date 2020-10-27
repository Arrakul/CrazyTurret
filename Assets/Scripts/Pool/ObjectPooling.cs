using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private List<PoolObject> _objects;
    private Transform _objectsParent;
    
    public void Initialize (int count, PoolObject sample, Transform objects_parent)
    {
        _objects = new List<PoolObject>();
        _objectsParent = objects_parent;
        
        for (int i=0; i<count; i++) 
        {
            AddObject(sample, objects_parent);
        }
    }

    public PoolObject GetObject () 
    {
        foreach (var item in _objects)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                return item;
            }
        }
        
        AddObject(_objects[0], _objectsParent);
        return _objects[_objects.Count-1];
    }
    
    public PoolObject GetActiveObject () 
    {
        foreach (var item in _objects)
        {
            if (item.gameObject.activeSelf)
            {
                return item;
            }
        }
        return null;
    }
    
    void AddObject(PoolObject sample, Transform objects_parent) 
    {
        GameObject temp = GameObject.Instantiate(sample.gameObject);
        temp.name = sample.name;
        temp.transform.SetParent(objects_parent);
        _objects.Add(temp.GetComponent<PoolObject>());
        temp.SetActive(false);
    }
}
