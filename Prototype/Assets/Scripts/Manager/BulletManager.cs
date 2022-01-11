using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    // pool list
    private Dictionary<string, List<GameObject>> Pool;

    private void Start()
    {
        // init pool
        Pool = new Dictionary<string, List<GameObject>>();

        Pool = new Dictionary<string, List<GameObject>>();

    }
    // pool function
    public GameObject GenerateBulletFromPool(GameObject item, Transform parent, Vector3 position, ref Transform target)
    {
        if (Pool.ContainsKey(item.name))
        {
            // if item available in pool
            if (Pool[item.name].Count > 0)
            {
                GameObject newItemFromPool = Pool[item.name][0];
                Pool[item.name].Remove(newItemFromPool);

                newItemFromPool.SetActive(true);
                newItemFromPool.transform.position = position;

                if (newItemFromPool.GetComponent<Bullet>() != null && target != null)
                {
                    newItemFromPool.GetComponent<Bullet>().setTarget(ref target);
                }

                return newItemFromPool;
            }
        }
        else
        {
            // if item list not defined, create new one
            Pool.Add(item.name, new List<GameObject>());
        }


        // create new one if no item available in pool
        GameObject newItem = Instantiate(item, parent);
        newItem.transform.position = position;
        if (newItem.GetComponent<Bullet>() != null && target != null)
        {
            newItem.GetComponent<Bullet>().setTarget(ref target);
        }

        newItem.name = item.name;
        return newItem;
    }

    public GameObject GenerateVXFromPool(GameObject item, Transform parent, Vector3 position, Quaternion rotation)
    {

        if (Pool.ContainsKey(item.name))
        {
            // if item available in pool
            if (Pool[item.name].Count > 0)
            {
                GameObject newItemFromPool = Pool[item.name][0];
                Pool[item.name].Remove(newItemFromPool);

                newItemFromPool.SetActive(true);
                newItemFromPool.transform.position = position;
                newItemFromPool.transform.rotation = rotation;
                return newItemFromPool;
            }
        }
        else
        {
            // if item list not defined, create new one
            Pool.Add(item.name, new List<GameObject>());
        }


        // create new one if no item available in pool
        GameObject newItem = Instantiate(item, parent);
        newItem.transform.position = position;
        newItem.transform.rotation = rotation;

        newItem.name = item.name;
        return newItem;
    }
    public void ReturnToPool(GameObject item)
    {
        if (!Pool.ContainsKey(item.name) && (!Pool.ContainsKey(item.name)))
        {
            Debug.LogError("INVALID POOL ITEM!!");
            return;
        } 


        Pool[item.name].Add(item);
        item.SetActive(false);
    }
}
