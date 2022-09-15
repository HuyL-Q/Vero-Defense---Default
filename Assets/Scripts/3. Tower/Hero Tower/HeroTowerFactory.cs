using UnityEngine;

public class HeroTowerFactory : AbstractFactoryTower
{
    [SerializeField]
    GameObject basicTowerPrefab;

    public override void CreateTower(Transform tower, Vector3 position, int placementIndex)
    {
        Debug.Log(position + " " + placementIndex);
        var towerGO = Instantiate(basicTowerPrefab, tower);
        towerGO.transform.position = position;
        towerGO.GetComponent<HeroTower>().PlacementIndex = placementIndex;
        //tower.GetComponent<ArcherTower>().SetTower(id);
        //Debug.Log(PlayerStats.Money);//UI Design
    }

    public override void CreateTower(Transform tower, Vector3 position, int placementIndex, string id)
    {
        var towerGO = Instantiate(basicTowerPrefab, tower);
        towerGO.transform.position = position;
        towerGO.GetComponent<HeroTower>().PlacementIndex = placementIndex;
        StartCoroutine(towerGO.GetComponent<HeroTower>().SetTower(id));
        //towerGO.GetComponent<SpriteRenderer>().sprite//level up
        string[] idls = id.Split('_');  
        Transform towerLevelSprite = towerGO.transform.GetChild(0);
        for(int i = 0; i < towerLevelSprite.childCount; i++)
        {
            towerLevelSprite.GetChild(i).gameObject.SetActive(false);
        }
        towerLevelSprite.GetChild(int.Parse(idls[2]) - 1).gameObject.SetActive(true);
        if (towerLevelSprite.GetChild(int.Parse(idls[2]) - 1).childCount > 0)
            towerLevelSprite.GetChild(int.Parse(idls[2]) - 1).GetChild(int.Parse(idls[3]) - 1).gameObject.SetActive(true);
    }
}
