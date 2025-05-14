using System.Collections.Generic;
using UnityEngine;


namespace Subvrsive.Combat.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;

        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                GameObject objectParrent = Instantiate(new GameObject(pool.tag + " Pool"));
                objectParrent.transform.SetParent(transform);

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.transform.SetParent(objectParrent.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag)) return null;

            GameObject obj = poolDictionary[tag].Dequeue();

            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(obj);

            return obj;
        }

        public void ReturnToPool(string tag, GameObject obj)
        {
            if (!poolDictionary.ContainsKey(tag)) return;
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
    }
}

