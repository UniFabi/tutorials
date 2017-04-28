using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    PooledObject prefab;
    List<PooledObject> availableObjects = new List<PooledObject>();

    public PooledObject GetObject () {
        int lastAvailableIndex = availableObjects.Count - 1;
        PooledObject obj;
        if(lastAvailableIndex < 0){
            obj = Instantiate<PooledObject>(prefab);
            obj.transform.SetParent(transform,false);
        }
        else{
            obj = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void AddObject (PooledObject obj){
        obj.gameObject.SetActive(false);
        availableObjects.Add(obj);
    }

    public static ObjectPool GetPool(PooledObject prefab){
        GameObject obj;
        ObjectPool pool;

        if(Application.isEditor){
            obj = GameObject.Find(prefab.name + " Pool");
            if(obj){
                pool = obj.GetComponent<ObjectPool>();
                    if(pool){
                        return pool;
                    }
            }
        }

        obj = new  GameObject(prefab.name + " Pool");
        DontDestroyOnLoad(obj);
        pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }


}
