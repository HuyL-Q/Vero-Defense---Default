using UnityEngine;

public class MinionFactory : AbstractFactoryEnemy
{
    public override void createEnemy(string id)
    {

    }

    public void CreateEnemy(GameObject spawnPos, GameObject data)
    {
        GameObject minionsGameObject = ObjectPoolController.Instance.Pools[data.name].Get();
        //minionsGameObject.GetComponent<AEnemy>().Agent.updatePosition = false;//?
        Debug.Log(spawnPos.transform.position);
        minionsGameObject.transform.position = spawnPos.transform.position;
        minionsGameObject.GetComponent<AEnemy>().Agent.nextPosition = spawnPos.transform.position;//?
        minionsGameObject.SetActive(true);
    }
} 