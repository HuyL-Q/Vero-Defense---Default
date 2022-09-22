using UnityEngine;
using static ATower;

public class ArcherTowerFactory : AbstractFactoryTower
{
    [SerializeField]
    GameObject basicTowerPrefab;

    public override void CreateTower(Transform tower, Vector3 position, int placementIndex)
    {
        var towerGO = Instantiate(basicTowerPrefab, tower);
        towerGO.transform.position = position;
        towerGO.GetComponent<ArcherTower>().PlacementIndex = placementIndex;
        GameController.instance.PlayerMoney -= towerGO.GetComponent<ArcherTower>().Price;
        StoryUIController.instance.UpdateGoldIndex();
    }

    public override void CreateTower(Transform tower, Vector3 position, int placementIndex, string id)
    {
        var towerGO = Instantiate(basicTowerPrefab, tower);
        towerGO.transform.position = position;
        towerGO.GetComponent<ArcherTower>().PlacementIndex = placementIndex;
        StartCoroutine(towerGO.GetComponent<ArcherTower>().SetTower(id));
        GameController.instance.PlayerMoney -= towerGO.GetComponent<ArcherTower>().Price;
        StoryUIController.instance.UpdateGoldIndex();
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
