using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionFactory : AbstractFactoryEnemy
{
    public override void createEnemy(string id)
    {
        
    }

    public void CreateEnemy(GameObject spawnPos)
    {
        GameObject minionsGameObject = ObjectPool.SharedInstance.GetPooledObject("Minions");
        if (minionsGameObject != null)
        {
            minionsGameObject.transform.position = spawnPos.transform.position;
            minionsGameObject.SetActive(true);
        }
        else
        {
            throw new System.ArgumentException("Cannot create Enemy.");
        }
    }
}