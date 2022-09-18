using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame
{
    public int lives = 0;
    public float money = 0;
    public float playerPoint = 0;
    public State state = new State();
    public int stageIndex = 0;
    public List<TowerData> ls = new List<TowerData>();
    public static SaveGame Instance = new SaveGame();

}
public class TowerData
{
    public float x;
    public float y;
    public float z;
    public int towerPlacementIndex;
    public string id;
    public TowerData() { }
    public TowerData(GameObject Tower)
    {
        x = Tower.transform.position.x;
        y = Tower.transform.position.y;
        z = Tower.transform.position.z;
        towerPlacementIndex = Tower.GetComponent<ATower>().PlacementIndex;
        id = Tower.GetComponent<ATower>().ID;
    }
    public override string ToString()
    {
        return $"{this.x} + {this.y} + {this.z} + {this.towerPlacementIndex} + {this.id}";
    }
}