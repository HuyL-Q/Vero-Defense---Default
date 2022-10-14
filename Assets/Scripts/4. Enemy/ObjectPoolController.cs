using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolController : MonoBehaviour
{
    public static ObjectPoolController Instance;
    public Dictionary<string, ObjectPool<GameObject>> Pools = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pool in NewSpawnController.Instance.EnemyPrefabs)
        {
            Pools.Add(pool.name, new ObjectPool<GameObject>(
                () => Instantiate(pool, GameObject.Find("Enemy").transform),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj => Destroy(obj),
                false
                ));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
