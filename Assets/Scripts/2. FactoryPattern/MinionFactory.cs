using UnityEngine;

public class MinionFactory : AbstractFactoryEnemy
{
    public override void createEnemy(string id)
    {

    }

    public void CreateEnemy(GameObject spawnPos, GameObject data)
    {
        GameObject minionsGameObject = ObjectPoolController.Instance.Pools[data.name].Get();
        minionsGameObject.GetComponent<AEnemy>().Agent.Warp(spawnPos.transform.position);
        if (int.Parse(spawnPos.name.Split("Spawn")[1]) <= 2)
        {
            minionsGameObject.GetComponent<AEnemy>().Agent.destination = GameObject.Find("Destination").transform.position;
        }
        else
        {
            minionsGameObject.GetComponent<AEnemy>().Agent.destination = GameObject.Find("Destination2").transform.position;
        }
        minionsGameObject.SetActive(true);
    }
}