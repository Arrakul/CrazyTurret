using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Transform _objectsParent;
    
    private Queue<PoolObject> _activeObjectsQueue;
    private Queue<PoolObject> _notActiveObjectsQueue;
    private PoolObject standartObject;

    private void Start()
    {
        StartCoroutine(ReturnObject());
    }

    public void Initialize (int count, PoolObject sample, Transform objects_parent)
    {
        _activeObjectsQueue = new Queue<PoolObject>();
        _notActiveObjectsQueue = new Queue<PoolObject>();
        
        _objectsParent = objects_parent;
        standartObject = sample;
        
        for (int i = 0; i < count; i++) 
        {
            AddObject(sample, objects_parent);
        }
    }

    void AddObject (PoolObject sample, Transform objects_parent) 
    {
        GameObject temp = Instantiate(sample.gameObject);
        temp.name = sample.name;
        temp.transform.SetParent(objects_parent);
        
        _notActiveObjectsQueue.Enqueue(temp.GetComponent<PoolObject>());
        temp.SetActive(false);
    }
    
    public PoolObject GetObject ()
    {
        PoolObject obj;
        
        if (_notActiveObjectsQueue.Count > 0)
        {
            obj = _notActiveObjectsQueue.Dequeue();
            _activeObjectsQueue.Enqueue(obj);
        }
        else
        {
            AddObject(standartObject, _objectsParent);
            obj = _notActiveObjectsQueue.Dequeue();
            _activeObjectsQueue.Enqueue(obj);
        }

        return obj;
    }
    
    public PoolObject GetActiveObject ()
    {
        if (_activeObjectsQueue.Count > 0)
        {
            return _activeObjectsQueue.Dequeue();
        }

        return null;
    }
    
    IEnumerator ReturnObject()
    {
        while (true)
        {
            if (_activeObjectsQueue.Count > 0)
            {
                if (!_activeObjectsQueue.Peek().gameObject.activeInHierarchy)
                {
                    _notActiveObjectsQueue.Enqueue(_activeObjectsQueue.Dequeue());
                }
            }
            yield return  new WaitForFixedUpdate();
        }
    }
}
