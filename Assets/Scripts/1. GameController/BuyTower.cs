using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton
{
    public string type;
}
public class TowerButtonConverter : JsonConverter<List<TowerButton>> { }
public class BuyTower : MonoBehaviour
{
    public Transform Content;
    public GameObject ButtonPrefab;
    public static List<GameObject> ButtonList;
    public static bool flag = false;
    public List<string> btnName = new();
    
    // Start is called before the first frame update
    void Start()
    {
        ButtonList = new List<GameObject>();
        //TowerButtonConverter tbc = new();
        //tbc.setCurrentDir("/TowerButton.json");
        //List<TowerButton> ls = tbc.getObjectFromJSON();
        foreach(string t in btnName)
        {
            GameObject button = Instantiate(ButtonPrefab,Content);
            Button btn = button.GetComponent<Button>();
            btn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Prefabs/TowerSprites/" + t);
            btn.name = t;
            btn.onClick.AddListener(()=> StoryUIController.instance.BuildTower(t));
            ButtonList.Add(button);
            if (t.Contains("hero") && !GameController.instance.HeroList.ContainsKey(int.Parse(t.Split("_")[2])))
            {
                GameController.instance.HeroList.Add(int.Parse(t.Split("_")[2]), false);
            }
            flag = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
