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
    public static List<GameObject> ButtonList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        TowerButtonConverter tbc = new();
        tbc.setCurrentDir("/TowerButton.json");
        List<TowerButton> ls = tbc.getObjectFromJSON();
        foreach(TowerButton t in ls)
        {
            GameObject button = Instantiate(ButtonPrefab,Content);
            Button btn = button.GetComponent<Button>();
            btn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Prefabs/TowerSprites/" + t.type);
            btn.onClick.AddListener(()=> StoryUIController.instance.BuildTower(t.type));
            ButtonList.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void function() { }
}
