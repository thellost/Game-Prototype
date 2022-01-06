using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    // pool list
    private Dictionary<string, List<GameObject>> pool;
    
    private void Start()
    {
        // init pool
        pool = new Dictionary<string, List<GameObject>>();
       
    }
    // pool function
    public GameObject GenerateFromPool(GameObject item, Transform parent, Vector3 position, ref Transform target)
    {

        if (pool.ContainsKey(item.name))
        {
            Debug.Log(pool[item.name].Count);
            // if item available in pool
            if (pool[item.name].Count > 0)
            {
                GameObject newItemFromPool = pool[item.name][0];
                pool[item.name].Remove(newItemFromPool);
               
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
            pool.Add(item.name, new List<GameObject>());
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
    public void ReturnToPool(GameObject item)
    {
        if (!pool.ContainsKey(item.name))
        {
            Debug.LogError("INVALID POOL ITEM!!");
        }


        pool[item.name].Add(item);
        item.SetActive(false);
    }
}
