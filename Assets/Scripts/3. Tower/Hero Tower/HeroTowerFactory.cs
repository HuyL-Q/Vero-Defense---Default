using UnityEngine;

public class HeroTowerFactory : AbstractFactoryTower
{
    [SerializeField]
    GameObject basicTowerPrefab;

    [SerializeField]
    GameObject[] heroTowerPrefab;

    public override void CreateTower(Transform tower, Vector3 position, int placementIndex)
    {
        Debug.Log(position + " " + placementIndex);
        var towerGO = Instantiate(basicTowerPrefab, tower);
        towerGO.transform.position = position;
        towerGO.GetComponent<HeroTower>().PlacementIndex = placementIndex;   
    }

    //public  void CreateTower(Transform tower, Vector3 position, int placementIndex, int index)
    //{
    //    Debug.Log(position + " " + placementIndex);
    //    var towerGO = Instantiate(heroTowerPrefab[index-1], tower);
    //    towerGO.transform.position = position;
    //    towerGO.GetComponent<HeroTower>().PlacementIndex = placementIndex;
    //    Debug.Log(towerGO.GetComponent<HeroTower>().Price);
    //    GameController.instance.PlayerMoney -= towerGO.GetComponent<HeroTower>().Price;
    //    StoryUIController.instance.UpdateGoldIndex();
    //}

    public override void CreateTower(Transform tower, Vector3 position, int placementIndex, string id)
    {
        string[] idls = id.Split('_');
        var towerGO = Instantiate(heroTowerPrefab[int.Parse(idls[2])-1], tower);
        towerGO.transform.position = position;
        towerGO.GetComponent<HeroTower>().PlacementIndex = placementIndex;
        StartCoroutine(towerGO.GetComponent<HeroTower>().SetTower(id));
        GameController.instance.PlayerMoney -= towerGO.GetComponent<HeroTower>().Price;
        StoryUIController.instance.UpdateGoldIndex();
        //towerGO.GetComponent<SpriteRenderer>().sprite//level up
          
        Transform towerLevelSprite = towerGO.transform.GetChild(0);
        for(int i = 0; i < towerLevelSprite.childCount; i++)
        {
            towerLevelSprite.GetChild(i).gameObject.SetActive(false);
        }
        towerLevelSprite.GetChild(0).gameObject.SetActive(true);
    }
    

}
